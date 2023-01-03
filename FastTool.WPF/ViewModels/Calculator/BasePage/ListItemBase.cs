#nullable disable
using FastTool.CalculationTool;
using FastTool.Utils;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace FastTool.WPF.ViewModels.Calculator
{
    public class ListItemBase : INotifyPropertyChanged
    {
        private string answer;
        private string expression = "";
        private int expThreshold = 4;
        private Mode mode;
        private int roundTo = 4;
        private TextBox textBox;
        private ObservableCollection<ValueViewModel> values;

        public string Answer
        {
            get => answer;
            protected set
            {
                answer = value;
                OnPropertyChanged();
            }
        }

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

        public ICommand GetTextBox => new RelayCommand(GetTextBoxExecute);

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

        public ListItemBase(ObservableCollection<ValueViewModel> values)
        {
            this.values = values;
        }

        protected virtual void Calculate()
        {
            new Thread(() =>
            {
                try
                {
                    Answer = new ExpressionParser(values.Select(v => new KeyValuePair<string, string>(v.Name, v.Expression)).ToList()).Parse(Expression).Calculate(Mode).ToStringSmart(expThreshold, roundTo);
                }
                catch
                {
                    Answer = "...";
                }
            }).Start();
        }

        private void GetTextBoxExecute(object obj)
        {
            textBox = obj as TextBox;
            textBox.TextChanged += TextBox_TextChanged;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Expression = textBox.Text;
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