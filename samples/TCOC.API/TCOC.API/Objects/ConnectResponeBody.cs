// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
using System;
using TCOC.API.Extensions;

namespace TCOC.API.Objects
{
    public class ConnectResponeBody : ISerializable
    {
        /// <summary>
        /// Trạng thái đăng nhập
        /// </summary>
        public ConnectStatus Status { get; set; }

        public byte[] Serialize()
        {
            throw new NotSupportedException();
        }

        public void Deserialize(byte[] bytes)
        {
            Status = (ConnectStatus)bytes.ToInt32(0);
        }
    }
}