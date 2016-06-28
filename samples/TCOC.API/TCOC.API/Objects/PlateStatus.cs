// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
namespace TCOC.API.Objects
{
    public enum PlateStatus
    {
        /// <summary>
        /// Không khớp biển số giữa Front End và Back End
        /// </summary>
        Mismatch        = 0,

        /// <summary>
        /// Khớp biển số
        /// </summary>
        Match           = 1,

        /// <summary>
        /// Không nhận diện được biển số
        /// </summary>
        Unrecognized    = 2
    }
}