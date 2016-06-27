// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Diagnostics;
using TCOC.API.Encrypt;
using TCOC.API.Extensions;
using TCOC.API.Objects;

namespace TCOC.API
{
  public class ConnectionErrorEventArgs
  {
    public Exception Exception { get; set; }
    public string Reason { get; set; }

    public ConnectionErrorEventArgs(string reason, Exception e)
    {
      Reason = reason;
      Exception = e;
    }
  }

  public class APIClient
  {
    public EventHandler<ConnectionErrorEventArgs> ConnectionError;
    public EventHandler<ConnectResponeBody> ConnectionSuccess;

    readonly System.Timers.Timer _timer;
    readonly SemaphoreSlim _semaphore;

    TcpClient _tcpClient;
    Stream _stream;
    ConnectRequestBody _connectBody;

    readonly string _clientIp;
    string _serverIp;
    readonly int _clientPort;
    int _requestId;
    int _errorCount;
    int _port;

    public int Session { get; private set; }
    public IEncrypter Encrypter { get; }

    /// <summary>
    /// </summary>
    /// <param name="clientIp"></param>
    /// <param name="clientPort"></param>
    /// <param name="handShakeInterval">Đơn vị giây</param>
    /// <param name="encryptKey">Khóa mã hóa</param>
    /// <param name="iv"></param>
    public APIClient(string clientIp, int clientPort, int handShakeInterval, string encryptKey, byte[] iv)
        : this(clientIp, clientPort, handShakeInterval, new AesEncrypter(encryptKey, iv))
    {
    }

    /// <summary>
    /// </summary>
    /// <param name="clientIp"></param>
    /// <param name="clientPort"></param>
    /// <param name="handShakeInterval">Đơn vị giây</param>
    /// <param name="encrypter"></param>
    public APIClient(string clientIp, int clientPort, int handShakeInterval, IEncrypter encrypter)
    {
      Encrypter = encrypter;
      _clientIp = clientIp;
      _clientPort = clientPort;
      _timer = new System.Timers.Timer
      {
        Interval = handShakeInterval * 1000,
        AutoReset = true,
        Enabled = true
      };
      _timer.Elapsed += NeedShake;
      _semaphore = new SemaphoreSlim(1);
    }

    async Task<T> Enqueue<T>(Func<Task<T>> taskGenerator)
    {
      await _semaphore.WaitAsync().ConfigureAwait(false);
      var res = await taskGenerator().ConfigureAwait(false);
      _semaphore.Release();
      return res;
    }

    /// <summary>
    /// Thực hiện gửi, nhận, mã hóa, giải mã các gói tin
    /// </summary>
    /// <typeparam name="T">Loại dữ liệu mong muốn nhận được</typeparam>
    /// <param name="req">Gói tin cần gửi đi</param>
    /// <returns></returns>
    async Task<T> InternalSendAsync<T>(IBase req) where T : ISerializable, new()
    {
      Exception exception = null;

      while (true)
      {
        try
        {
          var result = await HandleSendAsync<T>(req).ConfigureAwait(false);
          return result;
        }
        catch (Exception e)
        {
          _errorCount++;
          exception = e;
        }
        try
        {
          var result = await ConnectAsync().ConfigureAwait(false);
          if (result.Status == ConnectStatus.Success)
          {
            ConnectionSuccess?.Invoke(this, result);
            _errorCount = 0;
          }
          else
          {
            _errorCount++;
          }
        }
        catch (Exception e)
        {
          _errorCount++;
          exception = e;
        }
        if (_errorCount == 2)
        {
          ConnectionError?.Invoke(this, new ConnectionErrorEventArgs("Lỗi khi thử connect lại.", exception));
          _errorCount = 0;
        }
      }
      ConnectionError?.Invoke(this, new ConnectionErrorEventArgs("Gửi lệnh không thành công 5 lần.", null));
      return default(T);
    }

    async Task<T> HandleSendAsync<T>(IBase req) where T : ISerializable, new()
    {
      _timer.Stop();
      req.SessionId = Session;
      req.RequestId = _requestId++;
      var b = req.Serialize();
            Debug.WriteLine("DataLen: " + b.Length);
            for (int i = 0; i < b.Length; i++)
            {
                Debug.WriteLine(string.Format("{0:x}", b[i]));
            }
            Debug.WriteLine("\n");

            var encryptedBytes = Encrypter.Encrypt(b);
      var encryptedMessage = new List<byte>();
      encryptedMessage.AddRange((encryptedBytes.Length + 4).ToBytes());
      encryptedMessage.AddRange(encryptedBytes);
      var data = encryptedMessage.ToArray();

            Debug.WriteLine("DataLenEncrypt: " + data.Length);
            for (int i = 0; i < data.Length; i++)
            {
                Debug.WriteLine(string.Format("{0:x}", data[i]));
            }
            Debug.WriteLine("\n");
            var res = new Base<T>();

      await _stream.WriteAsync(data, 0, data.Length).ConfigureAwait(false);

      if (_tcpClient.Connected)
      {
        var lengthBytes = await _stream.ReadBytes(4).ConfigureAwait(false);
        var length = lengthBytes.ToInt32(0);
        var encryptedResponse = await _stream.ReadBytes(length - 4).ConfigureAwait(false);
        var repsonseBytes = Encrypter.Decrypt(encryptedResponse);
        res.Deserialize(repsonseBytes);
        Session = res.SessionId;
      }

      _timer.Start();
      return res.Body;
    }

    Task<T> SendAsync<T>(IBase req) where T : ISerializable, new()
        => Enqueue(() => InternalSendAsync<T>(req));

    async void NeedShake(object sender, ElapsedEventArgs e)
    {
      if (Session == 0) return;
      await ShakeAsync();
    }

    /// <summary>
    /// Là bản tin thực hiện thiết lập kết nối từ Front End đến Back End
    /// </summary>
    /// <param name="body"></param>
    /// <param name="serverIp"></param>
    /// <param name="port"></param>
    /// <returns></returns>
    public Task<ConnectResponeBody> ConnectAsync(string serverIp, int port, ConnectRequestBody body)
    {
      _serverIp = serverIp;
      _connectBody = body;
      _port = port;
      return ConnectAsync();
    }

    public async Task<ConnectResponeBody> ConnectAsync()
    {
      _tcpClient = new TcpClient(new IPEndPoint(IPAddress.Parse(_clientIp), _clientPort));
      _tcpClient.Connect(_serverIp, _port);
      _stream = _tcpClient.GetStream();
      var connectReq = new Base<ConnectRequestBody>
      {
        Length = 44,
        Command = Command.ConnectRequest,
        Body = _connectBody
      };
      _requestId = 1;
      return await SendAsync<ConnectResponeBody>(connectReq).ConfigureAwait(false);
    }

    /// <summary>
    /// Là bản tin Front End gửi BACK END khi có yêu cầu giữ kết kết nối đã đăng nhập
    /// </summary>
    /// <returns></returns>
    public Task<SessionResponseBody> ShakeAsync()
    {
      var req = new Base<NullRequestBody>
      {
        Length = 16,
        Command = Command.ShakeRequest,
        Body = new NullRequestBody()
      };
      return SendAsync<SessionResponseBody>(req);
    }

    /// <summary>
    /// Là bản tin BACK END trả về khi có yêu cầu hủy kết nối từ Front End
    /// </summary>
    /// <returns></returns>
    public async Task<SessionResponseBody> TerminateAsync()
    {
      var req = new Base<NullRequestBody>
      {
        Length = 16,
        Command = Command.TerminateRequest,
        Body = new NullRequestBody()
      };
      _timer.Stop();
      var re = await SendAsync<SessionResponseBody>(req).ConfigureAwait(false);
      _tcpClient.Close();

      return re;
    }

    /// <summary>
    /// Là bản tin Front End gửi đến Back End khi xe vào trạm để kiểm tra phương tiện có cho phép mở barie tự động theo ETC hay không
    /// </summary>
    /// <param name="body"></param>
    /// <returns></returns>
    public Task<CheckinResponseBody> CheckinAsync(ChargeRequestBody body)
    {
      var req = new Base<ChargeRequestBody>
      {
        Length = 78,
        Command = Command.CheckinRequest,
        Body = body
      };
      return SendAsync<CheckinResponseBody>(req);
    }

    /// <summary>
    /// Là bản tin Front End gửi khi muốn thực hiện trừ tiền tài khoản xe qua trạm
    /// </summary>
    /// <param name="body"></param>
    /// <returns></returns>
    public Task<TransactionResponseBody> CommitAsync(TransactionRequestBody body)
    {
      var req = new Base<TransactionRequestBody>
      {
        Length = 74,
        Command = Command.CommitRequest,
        Body = body
      };
      return SendAsync<TransactionResponseBody>(req);
    }

    /// <summary>
    /// Là bản tin Front End gửi đến Back End để hủy giao dịch giữ tiền đối với xe không qua trạm
    /// </summary>
    /// <param name="body"></param>
    /// <returns></returns>
    public Task<TransactionResponseBody> RollbackAsync(TransactionRequestBody body)
    {
      var req = new Base<TransactionRequestBody>
      {
        Length = 70,
        Command = Command.RollbackRequest,
        Body = body
      };
      return SendAsync<TransactionResponseBody>(req);
    }

    /// <summary>
    /// Là bản tin Front End gửi khi muốn thực hiện trừ tiền luôn tài khoản xe qua trạm
    /// </summary>
    /// <param name="body"></param>
    /// <returns></returns>
    public Task<ChargeResponseBody> ChargeAsync(ChargeRequestBody body)
    {
      var req = new Base<ChargeRequestBody>
      {
        Length = 48,
        Command = Command.ChargeRequest,
        Body = body
      };
      return SendAsync<ChargeResponseBody>(req);
    }
  }
}
