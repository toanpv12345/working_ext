using System;
using TCOC.API.Extensions;

namespace TCOC.API.Objects
{
    public class SessionResponseBody : ISerializable
    {
        /// <summary>
        /// Trạng thái bắt tay
        /// </summary>
        public SessionStatus Status { get; set; }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }

        public void Deserialize(byte[] bytes)
        {
            Status = (SessionStatus)bytes.ToInt32(0);
        }
    }
}