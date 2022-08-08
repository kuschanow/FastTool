using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FastTool.WPF
{
    public class MainCalculatorViewModel : INotifyPropertyChanged
    {
        public Calculator Calculator { get; set; } = new Calculator();

        private bool degMode = true;
        private bool radMode = false;
        private bool gradMode = false;

        public bool DegMode
        {
            get => degMode;
            set
            {
                degMode = value;
                OnPropertyChanged();
                if (value) Calculator.Mode = Mode.Deg;
            }
        }
        public bool RadMode
        {
            get => radMode;
            set
            {
                radMode = value;
                OnPropertyChanged();
                if (value) Calculator.Mode = Mode.Rad;
            }
        }
        public bool GradMode
        {
            get => gradMode;
            set
            {
                gradMode = value;
                OnPropertyChanged();
                if (value) Calculator.Mode = Mode.Grad;
            }
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
