// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
using System;
using TCOC.API.Extensions;

namespace TCOC.API.Objects
{
    public class StatusResponseBody : ISerializable
    {
        /// <summary>
        /// Trạng thái
        /// </summary>
        public SessionStatus SessionStatus { get; set; }

        public byte[] Serialize()
        {
            throw new NotSupportedException();
        }

        public void Deserialize(byte[] bytes)
        {
            SessionStatus = (SessionStatus)bytes.ToInt32(0);
        }
    }
}