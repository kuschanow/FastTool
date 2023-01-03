using FastTool.CalculationTool;
using FastTool.CalculationTool.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FastTool.WPF.Controls
{
    /// <summary>
    /// Логика взаимодействия для ResultItem.xaml
    /// </summary>
    public partial class ResultItem : UserControl
    {
        public ResultItem()
        {
            InitializeComponent();
            grid.DataContext = this;
        }

        #region Propertys

        public ICalculateble Expression
        {
            get => (ICalculateble)GetValue(ExpressionProperty);
            set => SetValue(ExpressionProperty, value);
        }

        public static readonly DependencyProperty ExpressionProperty = DependencyProperty.Register("Expression", typeof(ICalculateble), typeof(ResultItem));

        public string Answer
        {
            get => (string)GetValue(AnswerProperty);
            set => SetValue(AnswerProperty, value);
        }

        public static readonly DependencyProperty AnswerProperty = DependencyProperty.Register("Answer", typeof(string), typeof(ResultItem));

        public bool Expanded
        {
            get => (bool)GetValue(ExpandedProperty);
            set => SetValue(ExpandedProperty, value);
        }

        public static readonly DependencyProperty ExpandedProperty = DependencyProperty.Register("Expanded", typeof(bool), typeof(ResultItem));

        public int RoundTo
        {
            get => (int)GetValue(RoundToProperty);
            set => SetValue(RoundToProperty, value);
        }

        public static readonly DependencyProperty RoundToProperty = DependencyProperty.Register("RoundTo", typeof(int), typeof(ResultItem));

        public int ExpThreshold
        {
            get => (int)GetValue(ExpThresholdProperty);
            set => SetValue(ExpThresholdProperty, value);
        }

        public static readonly DependencyProperty ExpThresholdProperty = DependencyProperty.Register("ExpThreshold", typeof(int), typeof(ResultItem));

        public Mode Mode
        {
            get => (Mode)GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(Mode), typeof(ResultItem));

        #endregion
    }
}
