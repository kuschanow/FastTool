#nullable disable
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FastTool.WPF.ViewModels.Time
{
    public class SnapshotViewModel : INotifyPropertyChanged
    {
        private TimeSpan time;

        public TimeSpan Time => time;

        public SnapshotViewModel(TimeSpan time) => this.time = time;

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
