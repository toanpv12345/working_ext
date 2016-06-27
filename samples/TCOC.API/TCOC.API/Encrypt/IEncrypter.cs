// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
namespace TCOC.API.Encrypt
{
    public interface IEncrypter
    {
        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);
    }
}