using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FastTool.WPF.ViewModels.Calculator
{
    class BaseViewModel : INotifyPropertyChanged
    {
        public ICommand EqualsPress => new RelayCommand(EqualsPressExecute);

        private void EqualsPressExecute(object obj)
        {

        }

        public ICommand BackPress => new RelayCommand(BackPressExecute);

        private void BackPressExecute(object obj)
        {

        }

        public ICommand ClearPress => new RelayCommand(ClearPressExecute);

        private void ClearPressExecute(object obj)
        {

        }

        public ICommand ButtonPress => new RelayCommand(ButtonPressExecute);

        private void ButtonPressExecute(object obj)
        {

        }

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
