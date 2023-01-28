using FastTool.TimerTool.EventHandlers.Args;
using FastTool.TimerTool.EventHandlers.Hendlers;
using FastTool.TimerTool.Interfaces;
using System;
using System.Collections.Generic;
using System.Timers;

namespace FastTool.TimerTool;

public class Timer : ITimer
{
    private readonly System.Timers.Timer timer;
    private readonly System.Timers.Timer eventTimer;
    private readonly System.Diagnostics.Stopwatch stopwatch = new();

    private TimeSpan time;
    private bool autoReset;

    public List<ITimerAction> Actions { get; }
    public TimeSpan Time { get => time; set => time = value; }
    public TimeSpan LeftTime => Time - stopwatch.Elapsed;
    public bool AutoReset { get => autoReset; set => autoReset = value; }

    public Timer(double span, TimeSpan time, List<ITimerAction> actions, bool autoReset = false)
    {
        timer = new(span);
        eventTimer = new(time.TotalMilliseconds);
        AutoReset = autoReset;
        eventTimer.Elapsed += EventTimer_Elapsed;
        Time = time;
        Actions = actions;
    }

    public void Start()
    {
        eventTimer.Interval = LeftTime.TotalMilliseconds;
        eventTimer.Start();
        stopwatch.Start();
        timer.Start();
    }

    public void Pause()
    {
        eventTimer.Stop();
        stopwatch.Stop();
        timer.Stop();
    }

    public void Stop()
    {
        eventTimer.Stop();
        stopwatch.Reset();
        timer.Stop();
    }

    public void Restart()
    {
        Stop();
        Start();
    }

    private void EventTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        Actions.ForEach(a => a.Execute());
        EndTimer?.Invoke(this, new EventArgs());
        if (AutoReset)
            Restart();
        else
            Stop();
    }

    public event EventHandler EndTimer;

    public event ElapsedEventHandler Elapsed
    {
        add => timer.Elapsed += value;
        remove => timer.Elapsed -= value;
    }
}
