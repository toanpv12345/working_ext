// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
namespace TCOC.API.Objects
{
    public enum Command
    {
        ConnectRequest      = 0x00,
        ConnectResponse     = 0x01,
        ShakeRequest        = 0x02,
        ShakeResponse       = 0x03,
        CheckinRequest      = 0x04,
        CheckinResponse     = 0x05,
        CommitRequest       = 0x06,
        CommitResponse      = 0x07,
        RollbackRequest     = 0x08,
        RollbackResponse    = 0x09,
        TerminateRequest    = 0x0A,
        TerminateResponse   = 0x0B,
        ChargeRequest       = 0x0C,
        ChargeResponse      = 0x0D
    }
}