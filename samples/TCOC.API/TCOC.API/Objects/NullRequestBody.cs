// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
namespace TCOC.API.Objects
{
    /// <summary>
    /// Dùng cho các request không có nội dung. Như Shake, Terminate
    /// </summary>
    public class NullRequestBody : ISerializable
    {
        public byte[] Serialize()
        {
            return new byte[0];
        }

        public void Deserialize(byte[] bytes)
        {
            throw new System.NotImplementedException();
        }
    }
}