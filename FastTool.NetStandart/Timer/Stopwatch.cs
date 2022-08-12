using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace FastTool;

public class Stopwatch : ITimer
{
    private readonly System.Timers.Timer timer;

    #region props
    public TimeSpan Time { get; private set; }

    private DateTimeOffset TimerStart { get; set; }
    private DateTimeOffset TimerStop { get; set; }
    private TimeSpan StopedTime { get; set; }
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
        Time = TimeSpan.Zero;
        StopedTime = TimeSpan.Zero;
    }

    public void Update(object source, ElapsedEventArgs e)
    {
        Time = DateTimeOffset.Now - TimerStart - StopedTime;
        TimerUpdate?.Invoke(this, new TimerUpdateEventArgs(Time));
    }

    public event TimerUpdateEventHandler TimerUpdate;
}
