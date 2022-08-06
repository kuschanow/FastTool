using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool
{
    public class TimerEndEventArgs
    {
        public string Message { get; }
        public TimerEndEventArgs(string message) { Message = message; }
    }
}
