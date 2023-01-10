using FastTool.TimerTool.EventHandlers.Hendlers;
using System.Timers;

namespace FastTool.TimerTool.Interfaces
{
    internal interface ITimer
    {
        void Start();
        void Pause();
        void Stop();
        void Restart();
    }
}
