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

    private TimeSpan time;

    public TimeSpan Time 
    { 
        get => time; 
        private set 
        { 
            time = value; 
            TimerUpdate?.Invoke(this, new TimerUpdateEventArgs(Time)); 
        } 
    }

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
        timer.Stop();
        Time = TimeSpan.Zero;
    }

    public void Restart()
    {
        stopwatch.Restart();
        timer.Start();
    }

    public void Update(object source, ElapsedEventArgs e) => Time = stopwatch.Elapsed;

    public event TimerUpdateEventHandler TimerUpdate;
}
