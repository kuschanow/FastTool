using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace FastTool;

public class Timer : ITimer
{
    private readonly System.Timers.Timer timer;

    #region props
    public TimeSpan Time { get; }
    public string Message { get; }
    public TimeSpan TimeLeft { get; private set; }

    private DateTimeOffset TimerStart { get; set; }
    private DateTimeOffset TimerStop { get; set; }
    private TimeSpan stopedTime { get; set; }
    #endregion

    #region constructors
    public Timer(TimeSpan time) : this(time, "") { }
    public Timer(TimeSpan time, string message) : this(time, message, 1000) { }
    public Timer(TimeSpan time, string message, int span)
    {
        Time = time;
        Message = message;
        timer = new System.Timers.Timer(span);
        timer.Elapsed += Update;
    }
    #endregion

    public void Start()
    {
        timer.Start();
        TimerStart = DateTimeOffset.Now;
        if (TimerStop != null)
        {
            stopedTime += DateTimeOffset.Now - TimerStop;
        }
    }

    public void Stop()
    {
        timer.Stop();
        TimerStop = DateTimeOffset.Now;
    }

    public void Update(object source, ElapsedEventArgs e)
    {
        TimeLeft = DateTimeOffset.Now - TimerStart + stopedTime;
        TimerUpdate?.Invoke(this, new TimerUpdateEventArgs(Time));
        if (TimeLeft.Ticks <= 0)
        {
            TimerEnd?.Invoke(this, new TimerEndEventArgs(Message));
        }
    }

    public event TimerUpdateEventHandler TimerUpdate;
    public event TimerEndEventHandler TimerEnd;
}
