namespace FastTool.TimerTool.EventHandlers.Args
{
    public class TimerEndEventArgs
    {
        public string Message { get; }
        public TimerEndEventArgs(string message) { Message = message; }
    }
}
