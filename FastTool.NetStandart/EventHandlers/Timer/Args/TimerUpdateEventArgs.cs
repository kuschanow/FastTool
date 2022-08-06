using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool
{
    public class TimerUpdateEventArgs
    {
        public TimeSpan Time { get; } // readonly
        public TimerUpdateEventArgs(TimeSpan time) { Time = time; }
    }
}
