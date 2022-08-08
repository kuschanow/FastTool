using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
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
        private double? answer = null;

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

        public double? Answer
        {
            get => answer;
            private set
            {
                answer = value;
                OnPropertyChanged();
            }
        }

        public ICommand Calculate => new RelayCommand(CalculateExecute);
        public ICommand Copy => new RelayCommand(CopyExecute);

        private void CalculateExecute(object obj)
        {
            Expression Exp;
            if (obj != null)
            {
                Exp = new Expression(obj as string);
            }
            else
            {
                Exp = new Expression(Expression);
            }

            Answer = Calculator.Calculate(Exp);
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
