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
                if (value)
                {
                    degMode = true;
                    OnPropertyChanged();
                    Calculator.Mode = Mode.Deg;
                    RadMode = GradMode = false;
                }
                else
                {
                    if (RadMode || GradMode)
                    {
                        degMode = false;
                        OnPropertyChanged();
                    }
                }
            }
        }
        public bool RadMode
        {
            get => radMode;
            set
            {
                if (value)
                {
                    radMode = true;
                    OnPropertyChanged();
                    Calculator.Mode = Mode.Rad;
                    DegMode = GradMode = false;
                }
                else
                {
                    if (DegMode || GradMode)
                    {
                        radMode = false;
                        OnPropertyChanged();
                    }
                }
            }
        }
        public bool GradMode
        {
            get => gradMode;
            set
            {
                if (value)
                {
                    gradMode = true;
                    OnPropertyChanged();
                    Calculator.Mode = Mode.Grad;
                    DegMode = RadMode = false;
                }
                else
                {
                    if (DegMode || RadMode)
                    {
                        gradMode = false;
                        OnPropertyChanged();
                    }
                }
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
