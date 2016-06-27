// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
namespace TCOC.API.Objects
{
    public interface ISerializable
    {
        byte[] Serialize();
        void Deserialize(byte[] bytes);
    }
}