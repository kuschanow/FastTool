using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace FastTool.WPF
{
    public class MainCalculatorKeyboardViewModel : INotifyPropertyChanged
    {
        #region prors
        #endregion


        public ICommand Write => new RelayCommand(WriteExecute);

        private void WriteExecute(object obj)
        {
            OnKeyPressed((string)obj);
        }

        #region KeyPressed
        public event CalcKeyboardEventHendler KeyPressed;
        private void OnKeyPressed(string str)
        {
            KeyPressed?.Invoke(this, new CalcKeyboardEventArgs(str.Replace("#", ""), str.IndexOf('#')));
        }
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
