#nullable disable
using FastTool.CalculationTool;
using FastTool.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace FastTool.WPF.ViewModels.Calculator
{
    class BaseViewModel : INotifyPropertyChanged
    {
        private string expression = "";
        private Mode mode;
        private int roundTo = 4;
        private int expThreshold = 4;
        private TextBox textBox;
        private ScrollViewer scroll;
        private ObservableCollection<ResultViewModel> results = new();

        public string Expression
        {
            get => expression;
            set
            {
                expression = value;
                OnPropertyChanged();
            }
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

        public int RoundTo
        {
            get => roundTo;
            set
            {
                roundTo = value;
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

        public ObservableCollection<ResultViewModel> Results => results;

        public ICommand EqualsPress => new RelayCommand(EqualsPressExecute);

        private void EqualsPressExecute(object obj)
        {
            var parser = new ExpressionParser();

            var Exp = parser.Parse(Expression);

            var result = new ResultViewModel(Exp.Calculate(Mode).Round(RoundTo), Exp, Mode, RoundTo, ExpThreshold);

            results.Add(result);
            OnPropertyChanged(nameof(Results));

            scroll.ScrollToEnd();
        }

        public ICommand BackPress => new RelayCommand(BackPressExecute);

        private void BackPressExecute(object obj)
        {
            int index = textBox.CaretIndex;
            Expression = Expression.Remove(textBox.CaretIndex - 1, 1);

            textBox.CaretIndex = index - 1;
            textBox.Focus();
        }

        public ICommand ClearPress => new RelayCommand(ClearPressExecute);

        private void ClearPressExecute(object obj)
        {
            Expression = "";

            textBox.Focus();
        }

        public ICommand ButtonPress => new RelayCommand(ButtonPressExecute);

        private void ButtonPressExecute(object obj)
        {
            Expression = $"{Expression[0..textBox.CaretIndex]}{obj}{Expression[textBox.CaretIndex..]}";
            int index = Expression.IndexOf("#");
            Expression = Expression.Replace("#", "");
            try
            {
                textBox.CaretIndex = index;
            }
            catch
            {
                textBox.CaretIndex = textBox.Text.Length;
            }

            textBox.Focus();
        }

        public ICommand ModePress => new RelayCommand(ModePressExecute);

        private void ModePressExecute(object obj)
        {

        }

        public ICommand MemorySavePress => new RelayCommand(MemorySavePressExecute);

        private void MemorySavePressExecute(object obj)
        {

        }

        public ICommand HistoryClearPress => new RelayCommand(HistoryClearPressExecute);

        private void HistoryClearPressExecute(object obj)
        {
            results = new();
            OnPropertyChanged(nameof(Results));

        }

        public ICommand RoundToChanged => new RelayCommand(RoundToChangedExecute);

        private void RoundToChangedExecute(object obj)
        {

        }

        public ICommand ExpThresholdChanged => new RelayCommand(ExpThresholdChangedExecute);

        private void ExpThresholdChangedExecute(object obj)
        {

        }

        public ICommand GetTextBox => new RelayCommand(GetTextBoxExecute);

        private void GetTextBoxExecute(object obj)
        {
            textBox = obj as TextBox;
            textBox.TextChanged += TextBox_TextChanged;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Expression = textBox.Text;
        }

        public ICommand GetScroll => new RelayCommand(GetScrollExecute);

        private void GetScrollExecute(object obj)
        {
            scroll = obj as ScrollViewer;
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
