#nullable disable
using FastTool.TimerTool;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Timer = FastTool.TimerTool.Timer;

namespace FastTool.WPF.ViewModels.Time
{
    public class TimeViewModel : INotifyPropertyChanged
    {
        private Stopwatch stopWatch = new(10);
        private bool started;
        private bool paused;
        private bool resetted = true;
        private ObservableCollection<TimerViewModel> timers = new();
        private ObservableCollection<SnapshotViewModel> snapshots = new();


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

        public ObservableCollection<SnapshotViewModel> Snapshots => snapshots;
        public ObservableCollection<TimerViewModel> Timers => timers;

        public TimeViewModel()
        {
            stopWatch.Elapsed += StopWatch_Elapsed;
        }


        #region Stopwatch

        private void StopWatch_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            OnPropertyChanged(nameof(StopWatchTime));
        }

        public ICommand StartStopWatch => new RelayCommand(StartStopWatchExecute);

        private void StartStopWatchExecute(object obj) => stopWatch.Start();

        public ICommand PauseStopWatch => new RelayCommand(PauseStopWatchExecute);

        private void PauseStopWatchExecute(object obj) => stopWatch.Pause();

        public ICommand ResetStopWatch => new RelayCommand(ResetStopWatchExecute);

        private void ResetStopWatchExecute(object obj)
        {
            stopWatch.Stop();
            OnPropertyChanged(nameof(StopWatchTime));
        }

        public ICommand RestartStopWatch => new RelayCommand(RestartStopWatchExecute);

        private void RestartStopWatchExecute(object obj) => stopWatch.Restart();

        public ICommand TakeSnapshot => new RelayCommand(TakeSnapshotExecute);

        private void TakeSnapshotExecute(object obj) => snapshots.Add(new(stopWatch.Time));

        public ICommand ClearSnapshots => new RelayCommand(ClearSnapshotsExecute);

        private void ClearSnapshotsExecute(object obj) => snapshots.Clear();

        public ICommand DeleteSnapshot => new RelayCommand(DeleteSnapshotExecute);

        private void DeleteSnapshotExecute(object obj) => snapshots.Remove((SnapshotViewModel)obj);

        #endregion

        #region Timer

        public ICommand DeleteTimer => new RelayCommand(DeleteTimerExecute);

        private void DeleteTimerExecute(object obj) => timers.Remove((TimerViewModel)obj);

        public ICommand AddTimer => new RelayCommand(AddTimerExecute);

        private void AddTimerExecute(object obj) => timers.Add(new TimerViewModel(new TimeSpan(0, 0, 2)));

        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
