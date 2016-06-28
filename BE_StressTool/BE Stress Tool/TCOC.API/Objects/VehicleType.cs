// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
namespace TCOC.API.Objects
{
    public enum VehicleType
    {
        /// <summary>
        /// Xe dưới 12 chỗ, xe tải dưới 2 tấn; xe khách công cộng
        /// </summary>
        Type1 = 1,

        /// <summary>
        /// Xe 12-30 chỗ; xe tải 2 - 4 tấn
        /// </summary>
        Type2 = 2,

        /// <summary>
        /// Xe trên 31 chỗ; xe tải 4 - 10 tấn
        /// </summary>
        Type3 = 3,

        /// <summary>
        /// Xe 10 - 18 tấn; xe Container 20 fit
        /// </summary>
        Type4 = 4,
        
        /// <summary>
        /// Xe tải trên 18 tấn; xe Container 40 fit
        /// </summary>
        Type5 = 5
    }
}