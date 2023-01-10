#nullable disable
using FastTool.TimerTool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace FastTool.WPF.ViewModels.Time
{
    public class TimerViewModel : INotifyPropertyChanged
    {
        private Timer timer;
        private string name;
        private DateTimeOffset? endTime;
        private int hours;
        private int minutes;
        private int seconds;
        private bool isEditMode;
        private bool started;
        private bool paused;
        private bool resetted = true;
        private TimerAction timerAction;


        public TimerAction TimerAction
        {
            get => timerAction;
            set
            {
                timerAction = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public float T => (float)InverseLerp(0, Time.TotalMilliseconds, LeftTime.TotalMilliseconds);

        public DateTimeOffset? EndTime => endTime;

        public TimeSpan Time => timer.Time;

        public TimeSpan LeftTime => timer.LeftTime;

        public bool AutoReset
        {
            get => timer.AutoReset;
            set
            {
                timer.AutoReset = value;
                OnPropertyChanged();
            }
        }

        public int Hours
        {
            get => hours;
            set
            {
                hours = value;
                OnPropertyChanged();
            }
        }

        public int Minutes
        {
            get => minutes;
            set
            {
                minutes = value;
                OnPropertyChanged();
            }
        }

        public int Seconds
        {
            get => seconds;
            set
            {
                seconds = value;
                OnPropertyChanged();
            }
        }

        public bool IsEditMode
        {
            get => isEditMode;
            set
            {
                isEditMode = value;
                OnPropertyChanged();
                if (!value)
                {
                    timer.Time = new TimeSpan(Hours, Minutes, Seconds);
                    OnPropertyChanged(nameof(LeftTime));
                }
            }
        }

        public bool Started
        {
            get => started;
            set
            {
                started = value;
                OnPropertyChanged();
            }
        }

        public bool Paused
        {
            get => paused;
            set
            {
                paused = value;
                OnPropertyChanged();
            }
        }

        public bool Resetted
        {
            get => resetted;
            set
            {
                resetted = value;
                OnPropertyChanged();
            }
        }

        private double InverseLerp(double a, double b, double value)
        {
            return (value - a) / (b - a);
        }

        public TimerViewModel(TimeSpan time, Action<object> action = null, object parametr = null, bool autoreset = false)
        {
            timer = new(50, time, action, parametr, autoreset);
            timer.Elapsed += Timer_Elapsed;
            timer.EndTimer += Timer_EndTimer;
            hours = Time.Hours;
            minutes = Time.Minutes;
            seconds = Time.Seconds;
        }

        private void Timer_EndTimer(object sender, EventArgs e)
        {
            if (!AutoReset)
            {
                Resetted = true;
                Paused = false;
                Started = false;
                endTime = null;
                OnPropertyChanged(nameof(EndTime));
            }
            OnPropertyChanged(nameof(LeftTime));
            OnPropertyChanged(nameof(T));
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            OnPropertyChanged(nameof(LeftTime));
            OnPropertyChanged(nameof(T));
        }

        public ICommand Start => new RelayCommand(StartExecute);

        private void StartExecute(object obj)
        {
            timer.Start();
            OnPropertyChanged(nameof(LeftTime));
            endTime = DateTimeOffset.Now + LeftTime;
            OnPropertyChanged(nameof(EndTime));
        }

        public ICommand Pause => new RelayCommand(PauseExecute);

        private void PauseExecute(object obj)
        {
            timer.Pause();
            endTime = null;
            OnPropertyChanged(nameof(EndTime));
        }

        public ICommand Reset => new RelayCommand(ResetExecute);

        private void ResetExecute(object obj)
        {
            timer.Stop();
            Resetted = true;
            OnPropertyChanged(nameof(LeftTime));
            endTime = null;
            OnPropertyChanged(nameof(EndTime));
            OnPropertyChanged(nameof(T));
        }

        public ICommand Restart => new RelayCommand(RestartExecute);

        private void RestartExecute(object obj)
        {
            timer.Restart();
            endTime = DateTimeOffset.Now + LeftTime;
            OnPropertyChanged(nameof(EndTime));
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
