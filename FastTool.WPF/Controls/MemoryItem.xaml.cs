using FastTool.CalculationTool;
using FastTool.CalculationTool.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для MemoryItem.xaml
    /// </summary>
    public partial class MemoryItem : UserControl
    {
        public MemoryItem()
        {
            InitializeComponent();
            grid.DataContext = this;
        }

        #region Propertys

        public ICommand ApplyCommand
        {
            get => (ICommand)GetValue(ApplyCommandProperty);
            set => SetValue(ApplyCommandProperty, value);
        }

        public static readonly DependencyProperty ApplyCommandProperty = DependencyProperty.Register("ApplyCommand", typeof(ICommand), typeof(MemoryItem));

        public object ApplyProperty
        {
            get => GetValue(ApplyPropertyProperty);
            set => SetValue(ApplyPropertyProperty, value);
        }

        public static readonly DependencyProperty ApplyPropertyProperty = DependencyProperty.Register("ApplyProperty", typeof(object), typeof(MemoryItem));

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public object DeleteProperty
        {
            get => GetValue(DeletePropertyProperty);
            set => SetValue(DeletePropertyProperty, value);
        }

        public static readonly DependencyProperty DeletePropertyProperty = DependencyProperty.Register("DeleteProperty", typeof(object), typeof(MemoryItem));

        public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(MemoryItem));

        public string Expression
        {
            get => (string)GetValue(ExpressionProperty);
            set => SetValue(ExpressionProperty, value);
        }

        public static readonly DependencyProperty ExpressionProperty = DependencyProperty.Register("Expression", typeof(string), typeof(MemoryItem));

        public string Answer
        {
            get => (string)GetValue(AnswerProperty);
            set => SetValue(AnswerProperty, value);
        }

        public static readonly DependencyProperty AnswerProperty = DependencyProperty.Register("Answer", typeof(string), typeof(MemoryItem));

        public int RoundTo
        {
            get => (int)GetValue(RoundToProperty);
            set => SetValue(RoundToProperty, value);
        }

        public static readonly DependencyProperty RoundToProperty = DependencyProperty.Register("RoundTo", typeof(int), typeof(MemoryItem));

        public int ExpThreshold
        {
            get => (int)GetValue(ExpThresholdProperty);
            set => SetValue(ExpThresholdProperty, value);
        }

        public static readonly DependencyProperty ExpThresholdProperty = DependencyProperty.Register("ExpThreshold", typeof(int), typeof(MemoryItem));

        public Mode Mode
        {
            get => (Mode)GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(Mode), typeof(MemoryItem));

        public bool Expanded
        {
            get => (bool)GetValue(ExpandedProperty);
            set => SetValue(ExpandedProperty, value);
        }

        public static readonly DependencyProperty ExpandedProperty = DependencyProperty.Register("Expanded", typeof(bool), typeof(MemoryItem));

        #endregion

        #region Hendlers

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            ApplyCommand?.Execute(ApplyProperty);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteCommand?.Execute(DeleteProperty);
        }

        #endregion
    }
}
