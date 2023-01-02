#nullable disable
using FastTool.CalculationTool;
using FastTool.CalculationTool.Interfaces;
using FastTool.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FastTool.WPF.ViewModels.Calculator
{
    public class ResultViewModel : INotifyPropertyChanged
    {
        private Complex answer;
        private ICalculateble expression;
        private Mode mode;
        private int roundTo;
        private int expThreshold;

        public string Answer => answer.ToStringSmart(expThreshold, roundTo);

        public ICalculateble Expression 
        {
            get => expression;
            init => expression = value;
        }
        public Mode Mode 
        {
            get => mode; 
            set
            {
                mode = value;
                OnPropertyChanged();
                Calculate();
            }
        }
        public int RoundTo 
        { 
            get => roundTo; 
            set
            {
                roundTo = value;
                OnPropertyChanged();
                Calculate();
            }
        }

        public int ExpThreshold
        { 
            get => expThreshold; 
            set
            {
                expThreshold = value;
                OnPropertyChanged();
                Calculate();
            }
        }

        private void Calculate()
        {
            answer = Expression.Calculate(Mode);
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
