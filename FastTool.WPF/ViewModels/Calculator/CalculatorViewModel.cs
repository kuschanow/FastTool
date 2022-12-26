using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FastTool.WPF.ViewModels.Calculator
{
    class CalculatorViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged
#nullable disable
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
#nullable enable
        #endregion
    }
}
