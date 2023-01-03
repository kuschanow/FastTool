using FastTool.TimerTool.EventHandlers.Args;
using FastTool.TimerTool.EventHandlers.Hendlers;
using FastTool.TimerTool.Interfaces;
using System;
using System.Timers;

namespace FastTool.TimerTool;

public class Stopwatch : ITimer
{
    private readonly System.Timers.Timer timer;
    private readonly System.Diagnostics.Stopwatch stopwatch;

    public TimeSpan Time { get; private set; }

    public Stopwatch(int span)
    {
        timer = new System.Timers.Timer(span);
        timer.Elapsed += Update;
        stopwatch = new();
    }

    public void Start()
    {
        stopwatch.Start();
        timer.Start();
    }

    public void Pause()
    {
        stopwatch.Stop();
        timer.Stop();
    }

    public void Stop()
    {
        stopwatch.Reset();
        TimerUpdate?.Invoke(this, new TimerUpdateEventArgs(TimeSpan.Zero));
        timer.Stop();
    }

    public void Restart() => stopwatch.Restart();

    public void Update(object source, ElapsedEventArgs e)
    {
        Time = stopwatch.Elapsed;
        TimerUpdate?.Invoke(this, new TimerUpdateEventArgs(Time));
    }

    public event TimerUpdateEventHandler TimerUpdate;
}
