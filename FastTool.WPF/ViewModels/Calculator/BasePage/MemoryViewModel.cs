#nullable disable
using FastTool.CalculationTool;
using FastTool.CalculationTool.Functions;
using FastTool.Utils;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FastTool.WPF.ViewModels.Calculator
{
    public class MemoryViewModel : INotifyPropertyChanged
    {
        private string expression;
        private string answer;
        private Mode mode;
        private int roundTo;
        private int expThreshold;

        public string Expression
        {
            get => expression;
            set
            {
                expression = value;
                OnPropertyChanged();
                Calculate();
            }
        }

        public string Answer => answer;

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
            answer = new ExpressionParser().Parse(Expression).Calculate(Mode).ToStringSmart(expThreshold, roundTo);
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
