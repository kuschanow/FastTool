#nullable disable
using FastTool.TimerTool;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FastTool.WPF.ViewModels.Time
{
    public class TimeViewModel : INotifyPropertyChanged
    {
        private Stopwatch stopWatch = new(10);
        private bool started;
        private bool paused;
        private bool resetted = true;
        private ObservableCollection<StopWatchItemViewModel> stopWatchItems = new();

        public TimeSpan StopWatchTime => stopWatch.Time;

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

        public ObservableCollection<StopWatchItemViewModel> StopWatchItems => stopWatchItems;

        public TimeViewModel()
        {
            stopWatch.TimerUpdate += StopWatch_TimerUpdate;
        }

        private void StopWatch_TimerUpdate(object sender, TimerTool.EventHandlers.Args.TimerUpdateEventArgs e)
        {
            OnPropertyChanged(nameof(StopWatchTime));
        }

        public ICommand StartStopWatch => new RelayCommand(StartStopWatchExecute);

        private void StartStopWatchExecute(object obj)
        {
            stopWatch.Start();
        }

        public ICommand PauseStopWatch => new RelayCommand(PauseStopWatchExecute);

        private void PauseStopWatchExecute(object obj)
        {
            stopWatch.Pause();
        }

        public ICommand ResetStopWatch => new RelayCommand(ResetStopWatchExecute);

        private void ResetStopWatchExecute(object obj)
        {
            stopWatch.Stop();
        }

        public ICommand RestartStopWatch => new RelayCommand(RestartStopWatchExecute);

        private void RestartStopWatchExecute(object obj)
        {
            stopWatch.Stop();
            stopWatch.Start();
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
