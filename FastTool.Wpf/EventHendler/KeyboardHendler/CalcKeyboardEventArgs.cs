using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool.WPF
{
    public class CalcKeyboardEventArgs
    {
        public string Str { get; set; }
        public int CaretIndex { get; set; }

        public CalcKeyboardEventArgs(string str, int index)
        {
            this.Str = str;
            this.CaretIndex = index;
        }
    }
}
