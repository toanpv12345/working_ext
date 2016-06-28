// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
namespace TCOC.API.Objects
{
    public interface IBase : ISerializable
    {
        int Length { get; set; }
        Command Command { get; set; }
        int RequestId { get; set; }
        int SessionId { get; set; }
    }
}