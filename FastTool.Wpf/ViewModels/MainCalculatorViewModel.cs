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
        private double? ans = null;
        private double? prewAns = null;

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
                if (!string.IsNullOrWhiteSpace(Answer) && Answer != "Invalid expression")
                {
                    ans = prewAns;
                    Calculate.Execute(null);
                }
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
                if (!string.IsNullOrWhiteSpace(Answer) && Answer != "Invalid expression")
                {
                    ans = prewAns;
                    Calculate.Execute(null);
                }
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
                if (!string.IsNullOrWhiteSpace(Answer) && Answer != "Invalid expression")
                {
                    ans = prewAns;
                    Calculate.Execute(null);
                }
            }
        }
        public int Digits
        {
            get => Calculator.Digits;
            set
            {
                Calculator.Digits = value;
                OnPropertyChanged();
                if (!string.IsNullOrWhiteSpace(Answer) && Answer != "Invalid expression")
                {
                    ans = prewAns;
                    Calculate.Execute(null);
                }
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
        public string Ans
        {
            get
            {
                if (prewAns == null)
                {
                    return "Empty";
                }

                return prewAns.ToString();
            }
        }

        #endregion


        public ICommand Calculate => new RelayCommand(CalculateExecute);
        public ICommand Copy => new RelayCommand(CopyExecute);
        public ICommand Clear => new RelayCommand(ClearExecute);
        public ICommand GetExpField => new RelayCommand(GetExpFieldExecute);
        public ICommand ChangeMode => new RelayCommand(ChangeModeExecute);
        public ICommand ExpChanged => new RelayCommand(ExpChangedExecute);

        private void CalculateExecute(object obj)
        {
            try
            {
                prewAns = ans;

                if (obj != null)
                {
                    if (string.IsNullOrWhiteSpace(obj as string))
                    {
                        Answer = "";
                        return;
                    }
                    Expression = (string)obj;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(Expression))
                    {
                        Answer = "";
                        return;
                    }
                }

                if (Expression.ToLower().Contains("ans"))
                {
                    if (ans != null)
                    {
                        Expression = Expression.ToLower().Replace("ans", ans.ToString());
                    }
                    else
                    {
                        throw new Exception("Ans is Empty");
                    }
                }

                Expression Exp = new Expression(Expression);
                Answer = Calculator.Calculate(Exp).ToString();

                ans = Convert.ToDouble(Answer);
                OnPropertyChanged("Ans");
            }
            catch (Exception e)
            {
                Answer = e.Message;
                ans = null;
                OnPropertyChanged("Ans");
            }
            expField.Focus();
        }

        private void CopyExecute(object obj)
        {
            if (ExpField.SelectionLength > 0)
            {
                return;
            }

            string copyData;

            if (obj != null)
            {
                copyData = (string)obj;
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

        private void ExpChangedExecute(object obj)
        {
            Answer = "";
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
