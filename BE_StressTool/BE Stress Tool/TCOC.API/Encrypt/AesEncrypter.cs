// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TCOC.API.Encrypt
{
    public class AesEncrypter : IEncrypter
    {
        readonly byte[] _iv;
        readonly byte[] _password;

        public AesEncrypter(string password, byte[] iv)
        {
            _password = Encoding.ASCII.GetBytes(password);
            _iv = iv;
        }

        public byte[] Encrypt(byte[] data)
        {
            using (var ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.Padding = PaddingMode.PKCS7;
                    AES.Mode = CipherMode.CBC;
                    AES.BlockSize = 128;
                    AES.KeySize = 256;
                    AES.Key = _password;
                    AES.IV = _iv;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.Close();
                    }
                    return ms.ToArray();
                }
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.Padding = PaddingMode.PKCS7;
                    AES.Mode = CipherMode.CBC;
                    AES.BlockSize = 128;
                    AES.KeySize = 256;
                    AES.Key = _password;
                    AES.IV = _iv;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.Close();
                    }
                    return ms.ToArray();
                }
            }
        }
    }
}
