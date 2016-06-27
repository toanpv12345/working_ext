// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
using System;
using TCOC.API.Extensions;

namespace TCOC.API.Objects
{
    public class TransactionResponseBody : ISerializable
    {
        /// <summary>
        /// Trạng thái giao dịch
        /// </summary>
        public TransactionStatus Status { get; set; }

        public byte[] Serialize()
        {
            throw new NotSupportedException();
        }

        public void Deserialize(byte[] bytes)
        {
            Status = (TransactionStatus)bytes.ToInt32(0);
        }
    }
}