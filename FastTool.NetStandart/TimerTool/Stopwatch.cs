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

    public TimeSpan Time => stopwatch.Elapsed;

    public Stopwatch(double span)
    {
        timer = new System.Timers.Timer(span);
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
        timer.Stop();
    }

    public void Restart()
    {
        stopwatch.Restart();
        timer.Start();
    }

    public event ElapsedEventHandler Elapsed
    {
        add => timer.Elapsed += value;
        remove => timer.Elapsed -= value;
    }
}
