// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
using System.Text;
using TCOC.API.Extensions;

namespace TCOC.API.Objects
{
    public class CheckinResponseBody : ChargeResponseBody
    {
        /// <summary>
        /// Mã vé của giao dịch xe qua trạm, duy nhất trên toàn hệ thống, do Back End quản lý. Khi thực hiện trừ tiền hoặc khôi phục tiền giữ trên tài khoản, Front End cần phải trả về ticket id này
        /// </summary>
        public int TicketId { get; set; }

        public PlateType PlateType { get; set; }

        public override void Deserialize(byte[] bytes)
        {
            int pos = 0;
            Status = (TransactionStatus)bytes.ToInt32(pos);
            Etag = Encoding.ASCII.GetString(bytes, pos += 4, 24);
            Station = bytes.ToInt32(pos += 24);
            Lane = bytes.ToInt32(pos += 4);
            TicketId = bytes.ToInt32(pos += 4);
            TicketType = (TicketType)bytes.ToInt32(pos += 4);
            Price = bytes.ToInt32(pos += 4);
            VehicleType = (VehicleType)bytes.ToInt32(pos += 4);
            Plate = Encoding.ASCII.GetString(bytes, pos += 4, 10);
            PlateType = (PlateType)bytes.ToInt32(pos += 10);
        }
    }
}