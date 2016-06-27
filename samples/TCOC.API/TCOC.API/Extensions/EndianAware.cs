// 
// TCOC.API
// Nguyễn Trung Kiên (kiennt@trinam.com.vn)
// 
using System;
using System.Linq;
using System.Text;

namespace TCOC.API.Extensions
{
    public static class EndianAware
    {
        public static byte[] CorrectEndian(this byte[] bytes)
        {
            return BitConverter.IsLittleEndian 
                ? bytes 
                : bytes.Reverse().ToArray();
        }

        public static byte[] ToBytes(this string input)
        {
            return Encoding.ASCII.GetBytes(input).CorrectEndian();
        }

        public static byte[] ToBytes(this string input, int length)
        {
            var bytes = new byte[length];
            if (string.IsNullOrEmpty(input)) return bytes;
            var inputBytes = input.ToBytes();
            Array.Copy(inputBytes, bytes, inputBytes.Length > length ? length : inputBytes.Length);
            return bytes;
        }

        public static byte[] ToBytes(this int input)
        {
            return BitConverter.GetBytes(input).CorrectEndian();
        }

        public static byte[] ToBytes(this short input)
        {
            return BitConverter.GetBytes(input).CorrectEndian();
        }
        
        public static int ToInt32(this byte[] bytes, int start)
        {
            return BitConverter.ToInt32(bytes.Skip(start).Take(4).ToArray().CorrectEndian(), 0);
        }

        public static short ToInt16(this byte[] bytes, int start)
        {
            return BitConverter.ToInt16(bytes.Skip(start).Take(2).ToArray().CorrectEndian(), 0);
        }
    }
}