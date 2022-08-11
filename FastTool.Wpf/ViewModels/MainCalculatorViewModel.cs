using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FastTool.WPF
{
    public class MainCalculatorViewModel : INotifyPropertyChanged
    {
        public Calculator Calculator { get; set; } = new Calculator();

        private bool degMode = true;
        private bool radMode = false;
        private bool gradMode = false;
        private string expression = "";
        private TextBox expField;
        private string answer = "";

        public MainCalculatorViewModel()
        {
            MainKeyboard = new MainCalculatorKeyboardViewModel();
            MainKeyboard.KeyPressed += MainKeyboard_KeyPressed;
        }

        private void MainKeyboard_KeyPressed(object sender, CalcKeyboardEventArgs e)
        {
            int i = ExpCaretIngex;
            Expression = Expression.Insert(i, e.Str);
            ExpField.Focus();
            ExpCaretIngex = i + e.CaretIndex;
        }

        #region props
        public MainCalculatorKeyboardViewModel MainKeyboard { get; }
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
        public string Expression
        {
            get => expression;
            set
            {
                expression = value;
                OnPropertyChanged();
            }
        }
        private TextBox ExpField
        {
            get => expField;
        }
        public int ExpCaretIngex
        {
            get => ExpField.CaretIndex;
            set
            {
                ExpField.CaretIndex = value;
            }
        }
        public string Answer
        {
            get => answer;
            private set
            {
                answer = value;
                OnPropertyChanged();
            }
        }
        #endregion


        public ICommand Calculate => new RelayCommand(CalculateExecute);
        public ICommand Copy => new RelayCommand(CopyExecute);
        public ICommand Clear => new RelayCommand(ClearExecute);
        public ICommand GetExpField => new RelayCommand(GetExpFieldExecute);
        public ICommand ChangeMode => new RelayCommand(ChangeModeExecute);

        private void CalculateExecute(object obj)
        {
            try
            {
                Expression Exp;
                if (obj != null)
                {
                    if (string.IsNullOrWhiteSpace(obj as string))
                    {
                        Answer = "";
                        return;
                    }
                    Exp = new Expression(obj as string);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(Expression))
                    {
                        Answer = "";
                        return;
                    }
                    Exp = new Expression(Expression);
                }

                Answer = Calculator.Calculate(Exp).ToString();
            }
            catch
            {
                Answer = "Invalid expression";
            }
            expField.Focus();
        }

        private void CopyExecute(object obj)
        {
            string copyData;

            if (obj != null)
            {
                copyData = obj as string;
            }
            else
            {
                copyData = Answer.ToString();
            }

            Clipboard.SetText(copyData);
            expField.Focus();
        }

        private void ClearExecute(object obj)
        {
            Expression = "";
            Answer = "";
            expField.Focus();
        }

        private void ChangeModeExecute(object obj)
        {
            string mode = (string)obj;
            switch (mode)
            {
                case "deg":
                    DegMode = true;
                    break;
                case "rad":
                    RadMode = true;
                    break;
                case "grad":
                    GradMode = true;
                    break;
            }
            expField.Focus();
        }

        private void GetExpFieldExecute(object obj)
        {
            expField = (TextBox)obj;
            expField.Focus();
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

    }
}
