// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
namespace TCOC.API.Objects
{
    public enum ConnectStatus
    {
        Success = 0,

        /// <summary>
        /// Username hoặc mật khẩu không đúng
        /// </summary>
        Failed = 1,

        /// <summary>
        /// Vượt quá số lượng kết nối đồng thời cho phép
        /// </summary>
        TooManySimultaneousConnections = 2,

        /// <summary>
        /// Vượt quá số lượng kết nối đồng thời trong khoảng thời gian cho phép
        /// </summary>
        TooManySimultaneousConnections2 = 3,

        /// <summary>
        /// StationID truyền vào không đúng
        /// </summary>
        InvalidStationId = 4,

        /// <summary>
        /// Số lượng kết nối lỗi vượt quá giới hạn cho phép
        /// </summary>
        TooManyFailedConnectAttempts = 5
    }
}