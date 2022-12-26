#nullable disable
using System;
using System.Windows.Input;

namespace FastTool.WPF
{
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;
        public event EventHandler CanExecuteChanged;
        public RelayCommand(Action<object> execute) : this(execute, null) { }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute) { _execute = execute; _canExecute = canExecute; }
        public bool CanExecute(object parameter) => (_canExecute == null) ? true : _canExecute(parameter);
        public void Execute(object parameter) => _execute(parameter);
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}