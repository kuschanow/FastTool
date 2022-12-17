using System;

namespace FastTool.CalculationTool.EventHandlers.Args
{
    public class ChangeOperandsIndexEventArgs : EventArgs
    {
        public int ChangedPos { get; init; }

        public ChangeOperandsIndexEventArgs(int changedPos)
        {
            ChangedPos = changedPos;
        }
    }
}
