// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
using System;
using System.Collections.Generic;
using TCOC.API.Extensions;

namespace TCOC.API.Objects
{
    /// <summary>
    /// Dùng cho rollback và commit
    /// </summary>
    public class TransactionRequestBody : ISerializable
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
        /// Mã làn mà phương tiện đi qua tại trạm thu phí. Một số trạm mà cổng long môn đặt xa, có thể chưa xác định được chính xác mã làn khi xe vào trạm thì sẽ nhập vào là 0
        /// </summary>
        public int Lane { get; set; }

        /// <summary>
        /// Mã vé giao dịch Front End nhận được từ Back End khi Reserve
        /// </summary>
        public int TicketId { get; set; }

        /// <summary>
        /// Trạng thái khớp biển số
        /// </summary>
        public PlateStatus Status { get; set; }

        /// <summary>
        /// Biển số thực tế. Có thể là NULL nếu FrontEnd không nhận dạng được
        /// 10 Ký tự
        /// </summary>
        public string Plate { get; set; }
        
        /// <summary>
        /// Số lượng ảnh chụp liên quan
        /// </summary>
        public int ImageCount { get; set; }

        /// <summary>
        /// Chiều dài của phương tiện đi qua làn thu phí. Nếu không xác định được chiều dài, giá trị truyền vào bằng 0. Đơn vị là centimet (cm).
        /// </summary>
        public int VehicleLength { get; set; }

        public TransactionRequestBody() { }

        public TransactionRequestBody(string etag, int station, int lane, int ticketId, PlateStatus plateStatus, string plate, int imageCount)
        {
            Etag = etag;
            Station = station;
            Lane = lane;
            TicketId = ticketId;
            Status = plateStatus;
            Plate = plate;
            ImageCount = imageCount;
        }

        public byte[] Serialize()
        {
            var bytes = new List<byte>();
            bytes.AddRange(Etag.ToBytes(24));
            bytes.AddRange(Station.ToBytes());
            bytes.AddRange(Lane.ToBytes());
            bytes.AddRange(TicketId.ToBytes());
            bytes.AddRange(((int)Status).ToBytes());
            bytes.AddRange(Plate.ToBytes(10));
            bytes.AddRange(ImageCount.ToBytes());
            bytes.AddRange(VehicleLength.ToBytes());
            return bytes.ToArray();
        }

        public void Deserialize(byte[] bytes)
        {
            throw new NotSupportedException();
        }
    }
}