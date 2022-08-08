using System;
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

    public class RelayCommand : ICommand
    {
        private readonly Predicate<object>? _canExecute;
        private readonly Action<object> _execute;
        public event EventHandler? CanExecuteChanged;
        public RelayCommand(Action<object> execute) : this(execute, null) { }
        public RelayCommand(Action<object> execute, Predicate<object>? canExecute) { _execute = execute; _canExecute = canExecute; }
        public bool CanExecute(object parameter) => (_canExecute == null) ? true : _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
