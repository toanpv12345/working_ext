// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
namespace TCOC.API.Objects
{
    public enum TransactionStatus
    {
        /// <summary>
        /// Thành công
        /// </summary>
        Success                 = 0,

        /// <summary>
        /// Không tìm thấy thuê bao
        /// </summary>
        SubscriberNotFound      = 100,

        /// <summary>
        /// Thue bao không có hiệu lực
        /// </summary>
        SubscriberIsNotActive   = 101,

        /// <summary>
        /// Tài khoản không có hiệu lực
        /// </summary>
        AccountIsNotActive      = 102,

        /// <summary>
        /// Etag không có hiệu lực
        /// </summary>
        EtagIsNotActive         = 103,

        /// <summary>
        /// Tài khoản không có đủ tiền
        /// </summary>
        AccountNotEnoughMoney   = 104,

        /// <summary>
        /// Không tìm thấy giao dịch CHECKIN (khi COMMIT)
        /// </summary>
        CheckinInfoNotFound     = 200,

        /// <summary>
        /// Không tìm thấy giao dịch qua trạm kín (khi commit/rollback ở trạm vào hoặc trạm ra)
        /// </summary>
        CommitOrRollbackInfoNotFound = 201,

        /// <summary>
        /// Không tìm thấy giao dịch qua trạm kín (khi CHECKIN ở trạm ra)
        /// </summary>
        CheckinNotInfoFound2    = 202,

        /// <summary>
        /// Etag không hợp lệ (không phải trạng thái active)
        /// </summary>
        CheckinSessionTimeout   = 203,

        /// <summary>
        /// Trạng thái thuê bao không hợp lệ (không active)
        /// </summary>
        TollTypeNotFound        = 301,

        /// <summary>
        /// Tài khoản không hợp lệ (không active)
        /// </summary>
        StageNotFound           = 302,

        /// <summary>
        /// Loại trạm-Làn đường (thu phí) không hợp lệ
        /// </summary>
        PriceInfoNotFound       = 303,

        /// <summary>
        /// Giá tiền không hợp lệ
        /// </summary>
        PriceFormatIncorrect    = 304,

        ///// <summary>
        ///// Không đủ tiền trong tài khoản
        ///// </summary>
        //AccountInsufficient     = 2,

        ///// <summary>
        ///// Chưa khai bảng cước
        ///// </summary>
        //RateTableNotFound       = 4,

        ///// <summary>
        ///// Không tìm thấy thông tin giao dịch
        ///// </summary>
        //TransactionInfoNotfound = 5,

        ///// <summary>
        ///// Không tìm thấy giao dịch (áp dụng cho trạm kín)
        ///// </summary>
        //TransactionNotFound     = 6,

        ///// <summary>
        ///// Thông tin checkin không hợp lệ
        ///// </summary>
        //CheckinInfoIncorect     = 7,

        ///// <summary>
        ///// Không tìm thấy loại trạm
        ///// </summary>
        //StationTypeNotFound     = 10
    }
}