using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE_Stress_Tool
{
    class ComboboxItem
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public override string ToString()
        {
            return FileName;
        }

        public string GetFilePath()
        {
            return FilePath;
        }
    }
}
