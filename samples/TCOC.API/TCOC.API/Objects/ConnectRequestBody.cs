// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
using System;
using System.Collections.Generic;
using TCOC.API.Extensions;

namespace TCOC.API.Objects
{
    public class ConnectRequestBody : ISerializable
    {
        /// <summary>
        /// Tên đăng nhập
        /// 10 ký tự
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Mật khẩu
        /// 10 ký tự
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Mã trạm thu phí
        /// </summary>
        public int Station { get; set; }

        /// <summary>
        /// Thời gian timedout cho phép backend trả thông tin về
        /// </summary>
        public int Timeout { get; set; }

        public ConnectRequestBody() { }

        public ConnectRequestBody(string username, string password, int station)
        {
            Username = username;
            Password = password;
            Station = station;
            Timeout = 1000;
        }

        public byte[] Serialize()
        {
            var bytes = new List<byte>();
            bytes.AddRange(Username.ToBytes(10));
            bytes.AddRange(Password.ToBytes(10));
            bytes.AddRange(Station.ToBytes());
            bytes.AddRange(Timeout.ToBytes());
            return bytes.ToArray();
        }

        public void Deserialize(byte[] bytes)
        {
            throw new NotSupportedException();
        }
    }
}