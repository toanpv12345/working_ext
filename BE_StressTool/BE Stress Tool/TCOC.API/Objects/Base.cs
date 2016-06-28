// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
using System.Collections.Generic;
using System.Linq;
using TCOC.API.Extensions;

namespace TCOC.API.Objects
{
    public class Base<T> : IBase where T : ISerializable, new() 
    {
        /// <summary>
        /// Kích thước gói tin khi chưa mã hóa
        /// </summary>
        public int Length { get; set; }
        public Command Command { get; set; }
        public int RequestId { get; set; }
        public int SessionId { get; set; }

        public T Body { get; set; }
        
        public byte[] Serialize()
        {
            var bytes = new List<byte>();
            bytes.AddRange(Length.ToBytes());
            bytes.AddRange(((int)Command).ToBytes());
            bytes.AddRange(RequestId.ToBytes());
            bytes.AddRange(SessionId.ToBytes());
            bytes.AddRange(Body.Serialize());
            return bytes.ToArray();
        }
        
        public void Deserialize(byte[] bytes)
        {
            int pos = 0;
            Length = bytes.ToInt32(pos);
            Command = (Command) bytes[pos += 4];
            RequestId = bytes.ToInt32(pos += 4);
            SessionId = bytes.ToInt32(pos += 4);
            Body = new T();
            Body.Deserialize(bytes.Skip(pos+=4).ToArray());
        }
    }
}
