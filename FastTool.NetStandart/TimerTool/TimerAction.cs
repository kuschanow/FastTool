using FastTool.TimerTool.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastTool.TimerTool
{
    public class TimerAction : ITimerAction
    {
        public Action<object[]> Action { get; init; }

        public object[] Parameters { get; init; }

        public TimerAction(Action<object[]> action, params object[] parameters)
        {
            Action = action;
            Parameters = parameters;
        }

        public void Execute() => Action(Parameters);
    }
}
