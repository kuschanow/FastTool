using FastTool.TimerTool.EventHandlers.Args;
using FastTool.TimerTool.EventHandlers.Hendlers;
using FastTool.TimerTool.Interfaces;
using System;
using System.Timers;

namespace FastTool.TimerTool;

public class Timer : ITimer
{
    private readonly System.Timers.Timer timer;

    #region props
    public TimeSpan Time { get; }
    public string Message { get; }
    public TimeSpan TimeLeft { get; private set; }

    private DateTimeOffset TimerStart { get; set; }
    private DateTimeOffset TimerStop { get; set; }
    private TimeSpan StopedTime { get; set; }
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

    event TimerUpdateEventHandler ITimer.TimerUpdate
    {
        add
        {
            throw new NotImplementedException();
        }

        remove
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    public void Start()
    {
        if (!timer.Enabled)
        {
            TimerStart = DateTimeOffset.Now;
            if (StopedTime != TimeSpan.Zero)
            {
                StopedTime += DateTimeOffset.Now - TimerStop;
            }
            timer.Start();
        }
    }

    public void Pause()
    {
        if (timer.Enabled)
        {
            TimerStop = DateTimeOffset.Now;
            timer.Stop();
        }
    }

    public void Stop()
    {
        timer.Stop();
        TimeLeft = TimeSpan.Zero;
        StopedTime = TimeSpan.Zero;
    }

    public void Update(object source, ElapsedEventArgs e)
    {
        TimeLeft = Time - (DateTimeOffset.Now - TimerStart - StopedTime);
        if (TimeLeft.Ticks > 0)
        {
            TimerUpdate?.Invoke(this, new TimerUpdateEventArgs(Time));
        }
        else
        {
            TimeLeft = TimeSpan.Zero;
            TimerEnd?.Invoke(this, new TimerEndEventArgs(Message));
        }
    }

    public event TimerUpdateEventHandler TimerUpdate;
    public event TimerEndEventHandler TimerEnd;
}
