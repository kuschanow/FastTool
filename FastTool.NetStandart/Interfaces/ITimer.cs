using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace FastTool
{
    internal interface ITimer
    {
        void Start();
        void Stop();
        void Update(object source, ElapsedEventArgs e);
        event TimerUpdateEventHandler TimerUpdate;
        event TimerEndEventHandler TimerEnd;
    }
}
