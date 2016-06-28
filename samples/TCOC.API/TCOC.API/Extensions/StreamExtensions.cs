// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
using System;
using System.IO;
using System.Threading.Tasks;

namespace TCOC.API.Extensions
{
    public static class StreamExtensions
    {
        public static async Task<byte[]> ReadBytes(this Stream stream, int length)
        {
            byte[] bytes = new byte[length];
            int bytesLeft = length;
            while (bytesLeft > 0)
            {
                int nobr = await stream.ReadAsync(bytes, length - bytesLeft, bytesLeft).ConfigureAwait(false);
                if (nobr != -1)
                {
                    bytesLeft -= nobr;
                }
                else
                {
                    throw new Exception("Lỗi khi đọc dữ liệu từ socket");
                }
            }
            return bytes;
        }
    }
}
