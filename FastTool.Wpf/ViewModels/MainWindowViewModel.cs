using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace FastTool.WPF
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Visibility _mainWindowVisibility = Visibility.Visible;
        private Visibility _notifyIconVisibility = Visibility.Hidden;

        public MainCalculatorViewModel CalcViewModel { get; set; } = new MainCalculatorViewModel();
        public HotKeys HotKeys { get; set; }
        public Visibility MainWindowVisibility 
        { 
            get => _mainWindowVisibility; 
            set 
            { 
                _mainWindowVisibility = value;
                OnPropertyChanged();
                NotifyIconVisibility = MainWindowVisibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            }
        }
        public Visibility NotifyIconVisibility
        { 
            get => _notifyIconVisibility; 
            set 
            { 
                _notifyIconVisibility = value;
                OnPropertyChanged(); 
            } 
        }

        public MainWindowViewModel(MainWindow main)
        {
            HotKeys = new HotKeys(main);
        }

        public ICommand ChangeWindowVisibility => new RelayCommand(ChangeWindowVisibilityExecute);


        private void ChangeWindowVisibilityExecute(object obj)
        {
            MainWindowVisibility = MainWindowVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
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
