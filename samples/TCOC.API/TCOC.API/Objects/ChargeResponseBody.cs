// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
using System;
using System.Text;
using TCOC.API.Extensions;

namespace TCOC.API.Objects
{
    public class ChargeResponseBody : ISerializable
    {
        /// <summary>
        /// Thông tin TID của etag của phương tiện
        /// 24 ký tự
        /// </summary>
        public string Etag { get; set; }

        /// <summary>
        /// Mã trạm thu phí đang xử lý
        /// </summary>
        public int Station { get; set; }

        /// <summary>
        /// Làn thực tế khi xe đi qua (để hậu kiểm, xảy ra khi trường hợp đảo làn hoặc cổng long môn ở xa), trạm kín là mã làn vào, trạm hở bằng chính nó
        /// </summary>
        public int Lane { get; set; }

        /// <summary>
        /// Loại vé
        /// </summary>
        public TicketType TicketType { get; set; }

        /// <summary>
        /// Trạng thái giao dịch
        /// </summary>
        public TransactionStatus Status { get; set; }

        /// <summary>
        /// Giá cước sẽ bị trừ để Front End hiển thị trên bảng điện tử
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Loại phương tiện, để Front End hiển thị trên bảng điện tử
        /// </summary>
        public VehicleType VehicleType { get; set; }

        /// <summary>
        /// Biển số xe, để Front End hiển thị trên bảng điện tử hoặc phân tích sai lệch
        /// 10 ký tự
        /// </summary>
        public string Plate { get; set; }

        public byte[] Serialize()
        {
            throw new NotSupportedException();
        }

        public virtual void Deserialize(byte[] bytes)
        {
            int pos = 0;
            Etag = Encoding.ASCII.GetString(bytes, pos, 24);
            Station = bytes.ToInt32(pos += 24);
            Lane = bytes.ToInt32(pos += 4);
            TicketType = (TicketType)bytes.ToInt32(pos += 4);
            Status = (TransactionStatus)bytes.ToInt32(pos += 4);
            Price = bytes.ToInt32(pos += 4);
            VehicleType = (VehicleType)bytes.ToInt32(pos += 4);
            Plate = Encoding.ASCII.GetString(bytes, pos += 4, 10);
        }
    }
}