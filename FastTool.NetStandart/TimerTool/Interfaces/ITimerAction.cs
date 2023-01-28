using System;

namespace FastTool.TimerTool.Interfaces
{
    public interface ITimerAction
    {
        Action<object[]> Action { get; }

        object[] Parameters { get; }

        void Execute();
    }
}
