using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCOC.API.Extensions
{
    public static class Helper
    {
        public static string SubString(this string input, int maxLength)
        {
            if (input.Length > maxLength)
                return input.Substring(0, maxLength);
            return input;
        }
    }
}
