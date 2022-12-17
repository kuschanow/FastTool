using System;

namespace FastTool.TimerTool.EventHandlers.Args
{
    public class TimerUpdateEventArgs
    {
        public TimeSpan Time { get; } // readonly
        public TimerUpdateEventArgs(TimeSpan time) { Time = time; }
    }
}
