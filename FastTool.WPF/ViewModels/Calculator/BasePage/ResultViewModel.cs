#nullable disable
using FastTool.CalculationTool;
using FastTool.CalculationTool.Interfaces;
using FastTool.Utils;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FastTool.WPF.ViewModels.Calculator
{
    public class ResultViewModel : INotifyPropertyChanged
    {
        private Complex answer;
        private ICalculateble exp;
        private Mode mode;
        private int digit;
        private int expThreshold;

        public string Answer => answer.ToStringSmart(expThreshold, digit);

        public ICalculateble Exp 
        {
            get => exp;
            init => exp = value;
        }
        public Mode Mode 
        {
            get => mode; 
            set
            {
                mode = value;
                OnPropertyChanged();
            }
        }
        public int Digit 
        { 
            get => digit; 
            set
            {
                digit = value;
                OnPropertyChanged();
            }
        }

        public int ExpThreshold
        { 
            get => expThreshold; 
            set
            {
                expThreshold = value;
                OnPropertyChanged();
            }
        }

        public ResultViewModel(Complex answer, ICalculateble exp, Mode mode, int digit, int expThreshold)
        {
            this.answer = answer;
            Exp = exp;
            Mode = mode;
            Digit = digit;
            ExpThreshold = expThreshold;
        }

        public ICommand Calculate => new RelayCommand(CalculateExecute);

        private void CalculateExecute(object obj)
        {
            answer = Exp.Calculate(Mode);
            OnPropertyChanged(nameof(Answer));
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
