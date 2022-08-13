using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace FastTool.WPF
{
    public class TinyCalculatorViewModel : INotifyPropertyChanged
    {

        #region props
        public MainCalculatorViewModel Calc { get; set; } = new MainCalculatorViewModel();
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

    }
}
