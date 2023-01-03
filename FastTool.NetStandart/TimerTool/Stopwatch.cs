using FastTool.TimerTool.EventHandlers.Args;
using FastTool.TimerTool.EventHandlers.Hendlers;
using FastTool.TimerTool.Interfaces;
using System;
using System.Timers;

namespace FastTool.TimerTool;

public class Stopwatch : ITimer
{
    private readonly System.Timers.Timer timer;

    #region props
    public TimeSpan Time { get; private set; }

    private DateTimeOffset TimerStart { get; set; }
    private DateTimeOffset TimerStop { get; set; }
    private TimeSpan StopedTime { get; set; }
    private bool IsStopped { get; set; } = true;
    #endregion

    #region constructors
    public Stopwatch(int span)
    {
        timer = new System.Timers.Timer(span);
        timer.Elapsed += Update;
    }

    #endregion

    public void Start()
    {
        if (!timer.Enabled)
        {
            if (IsStopped)
            {
                IsStopped = false;
                TimerStart = DateTimeOffset.Now;
            }
            if (TimerStop != DateTimeOffset.MinValue)
                StopedTime += DateTimeOffset.Now - TimerStop;
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
        IsStopped = true;
        Time = TimeSpan.Zero;
        StopedTime = TimeSpan.Zero;
        TimerStop = DateTimeOffset.MinValue;
        TimerUpdate?.Invoke(this, new TimerUpdateEventArgs(Time));
    }

    public void Update(object source, ElapsedEventArgs e)
    {
        Time = DateTimeOffset.Now - TimerStart - StopedTime;
        TimerUpdate?.Invoke(this, new TimerUpdateEventArgs(Time));
    }

    public event TimerUpdateEventHandler TimerUpdate;
}
