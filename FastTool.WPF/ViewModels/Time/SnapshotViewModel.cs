#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
