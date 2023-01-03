#nullable disable
using FastTool.CalculationTool;
using FastTool.CalculationTool.Functions;
using FastTool.CalculationTool.Interfaces;
using FastTool.Utils;
using SQLitePCL;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
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
        private ObservableCollection<ValueViewModel> values = new();
        private ObservableCollection<ResultViewModel> results = new();
        private ObservableCollection<MemoryViewModel> memory = new();

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
        public ObservableCollection<MemoryViewModel> Memory => memory;
        public ObservableCollection<ValueViewModel> Values => values;

        public ICommand EqualsPress => new RelayCommand(EqualsPressExecute);

        private void EqualsPressExecute(object obj)
        {

            ICalculateble Exp;

            try
            {
                var vals = values.Select(v => new KeyValuePair<string, string>(v.Name, v.Expression)).ToList();
                vals.Add(new KeyValuePair<string, string>("ans", results.Count > 0 ? new GetComplex(new ICalculateble[] { new Number(results.Last().Expression.Calculate(Mode).Real), new Number(results.Last().Expression.Calculate(Mode).Imaginary) }).ToString() : ""));
                Exp = new ExpressionParser(vals).Parse(Expression);
            }
            catch
            {
                MessageBox.Show("Invalid input", "Input error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = new ResultViewModel() { Expression = Exp, Mode = Mode, RoundTo = RoundTo, ExpThreshold = ExpThreshold };

            results.Add(result);
            OnPropertyChanged(nameof(Results));
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
            Memory.Add(new MemoryViewModel(values) { Expression = Expression, Mode = Mode, RoundTo = RoundTo, ExpThreshold = ExpThreshold });
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

        public ICommand ApplyMemory => new RelayCommand(ApplyMemoryExecute);

        private void ApplyMemoryExecute(object obj)
        {
            Expression = $"{Expression[0..textBox.CaretIndex]}{((MemoryViewModel)obj).Expression}{Expression[textBox.CaretIndex..]}";
        }

        public ICommand ApplyValue => new RelayCommand(ApplyValueExecute);

        private void ApplyValueExecute(object obj)
        {
            var val = (ValueViewModel)obj;

            if (val.Error == Error.None)
                Expression = $"{Expression[0..textBox.CaretIndex]}{val.Name}{Expression[textBox.CaretIndex..]}";
        }

        public ICommand DeleteFromMemory => new RelayCommand(DeleteFromMemoryExecute);

        private void DeleteFromMemoryExecute(object obj)
        {
            memory.Remove((MemoryViewModel)obj);
        }

        public ICommand DeleteValue => new RelayCommand(DeleteValueExecute);

        private void DeleteValueExecute(object obj)
        {
            values.Remove((ValueViewModel)obj);
        }

        public ICommand AddValue => new RelayCommand(AddValueExecute);

        private void AddValueExecute(object obj)
        {
            values.Add(new ValueViewModel(values));
        }

        public ICommand ClearValue => new RelayCommand(ClearValueExecute);

        private void ClearValueExecute(object obj)
        {
            values.Clear();
        }

        public ICommand AddToMemory => new RelayCommand(AddToMemoryExecute);

        private void AddToMemoryExecute(object obj)
        {
            memory.Add(new MemoryViewModel(values));
        }

        public ICommand ClearMemory => new RelayCommand(ClearMemoryExecute);

        private void ClearMemoryExecute(object obj)
        {
            memory.Clear();
        }

        public ICommand NameChanged => new RelayCommand(NameChangedExecute);

        private void NameChangedExecute(object obj)
        {
            ((ValueViewModel)obj).Name = string.Join("", ((ValueViewModel)obj).Name.Where(ch => !"+-*/!^%".Contains(ch)));
            new Thread(() =>
            {
                var theSameValues = values.Select((v, i) =>
                {
                    var list = values.Skip(i + 1).Where(_v => !string.IsNullOrWhiteSpace(v.Name) && !string.IsNullOrWhiteSpace(_v.Name) && v.Name == _v.Name).ToList();
                    if (list.Count > 0)
                        list.Add(v);
                    return list;
                }).SelectMany(v => v);
                var reservedValues = values.Where(v => ExpressionParser.GetReservedNames().Contains(v.Name));
                var hasPrefix = values.Select(v => values.Where(_v => !string.IsNullOrWhiteSpace(v.Name) && !string.IsNullOrWhiteSpace(_v.Name) && _v != v && _v.Name != v.Name && _v.Name.StartsWith(v.Name))).SelectMany(v => v);

                if (theSameValues.Count() > 0)
                {
                    foreach (var v in theSameValues)
                        v.Error = Error.IdentityName;
                }

                if (reservedValues.Count() > 0)
                {
                    foreach (var v in reservedValues)
                        v.Error = Error.ReservedName;
                }

                if (hasPrefix.Count() > 0)
                {
                    foreach (var v in hasPrefix)
                        v.Error = Error.HasPrefix;
                }

                foreach (var v in values.Where(v => !theSameValues.Contains(v) && !reservedValues.Contains(v) && !hasPrefix.Contains(v) && v.Error != Error.None))
                    v.Error = Error.None;
            }).Start();
        }

        public ICommand ValueExpressionChanged => new RelayCommand(ValueExpressionChangedExecute);

        private void ValueExpressionChangedExecute(object obj)
        {
            new Thread(() =>
            {
                foreach (var item in values)
                    item.Expression = item.Expression;

                foreach (var item in memory)
                    item.Expression = item.Expression;
            }).Start();
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
