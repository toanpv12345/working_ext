// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
using System;
using System.Collections.Generic;
using TCOC.API.Extensions;

namespace TCOC.API.Objects
{
    public class ChargeRequestBody : ISerializable
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
        /// Làn thực tế khi xe đi qua (để hậu kiểm, xảy ra khi trường hợp đảo làn hoặc cổng long môn ở xa)
        /// </summary>
        public int Lane { get; set; }

        /// <summary>
        /// Biển số thực tế. Có thể là NULL nếu FrontEnd không nhận dạng được
        /// 10 Ký tự
        /// </summary>
        public string Plate { get; set; }

        /// <summary>
        /// TID của Etag mà Front End đọc được từ đầu đọc thẻ trong mỗi lần đọc. Đây là chuỗi 96 bít, và được chuyển dang dạng Text Hecxa. Nếu không xác định được, giá trị truyền vào là <rỗng>.
        /// Kích thước 12 bytes
        /// </summary>
        public string TID { get; set; }

        /// <summary>
        /// Giá trị bảo mật của thẻ Etag để phân biệt chính xác thẻ của VETC với các thẻ khác. Đây là chuỗi 64 bít, và được chuyển dang dạng Text Hecxa. Nếu không xác định được, giá trị truyền vào là <rỗng>.
        /// Kích thước 8 bytes
        /// </summary>
        public string HashValue { get; set; }

        public ChargeRequestBody() { }

        public ChargeRequestBody(string etag, int station, int lane)
        {
            Etag = etag;
            Station = station;
            Lane = lane;
        }

        public byte[] Serialize()
        {
            var bytes = new List<byte>();
            bytes.AddRange(Etag.ToBytes(24));
            bytes.AddRange(Station.ToBytes());
            bytes.AddRange(Lane.ToBytes());
            bytes.AddRange(Plate.ToBytes(10));
            bytes.AddRange(TID.ToBytes(12));
            bytes.AddRange(HashValue.ToBytes(8));
            return bytes.ToArray();
        }

        public void Deserialize(byte[] bytes)
        {
            throw new NotSupportedException();
        }
    }
}