using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace FastTool.WPF
{
    public class MainCalculatorKeyboardViewModel : INotifyPropertyChanged
    {
        private Visibility powOpt = Visibility.Collapsed;
        private Visibility rootOpt = Visibility.Collapsed;
        private Visibility trigonometry = Visibility.Collapsed;
        private Visibility func = Visibility.Visible;

        #region prors
        public Visibility PowOpt
        {
            get => powOpt;
            set
            {
                powOpt = value;
                OnPropertyChanged();
            }
        }
        public Visibility RootOpt
        {
            get => rootOpt;
            set
            {
                rootOpt = value;
                OnPropertyChanged();
            }
        }
        public Visibility Trigonometry
        {
            get => trigonometry;
            set
            {
                trigonometry = value;
                OnPropertyChanged();
            }
        }
        public Visibility Func
        {
            get => func;
            set
            {
                func = value;
                OnPropertyChanged();
            }
        }
        public bool TrigonometryKeyboard
        {
            get
            {
                if (Trigonometry == Visibility.Visible)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    Trigonometry = Visibility.Visible;
                }
                else
                {
                    Trigonometry = Visibility.Collapsed;
                }
            }
        }
        public bool FuncKeyboard
        {
            get
            {
                if (Func == Visibility.Visible)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    Func = Visibility.Visible;
                }
                else
                {
                    Func = Visibility.Collapsed;
                }
            }
        }
        #endregion


        public ICommand Write => new RelayCommand(WriteExecute);
        public ICommand ShowOpt => new RelayCommand(ShowOptExecute);

        private void WriteExecute(object obj)
        {
            OnKeyPressed((string)obj);
            PowOpt = Visibility.Collapsed;
            RootOpt = Visibility.Collapsed;
        }
        private void ShowOptExecute(object obj)
        {
            if ((string)obj == "pow")
            {
                PowOpt = Visibility.Visible;
                RootOpt = Visibility.Collapsed;
            }
            else if ((string)obj == "root")
            {
                RootOpt = Visibility.Visible;
                PowOpt = Visibility.Collapsed;
            }
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
