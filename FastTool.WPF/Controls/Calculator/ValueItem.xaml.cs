using FastTool.CalculationTool;
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
    /// Логика взаимодействия для ValueItem.xaml
    /// </summary>
    public partial class ValueItem : UserControl
    {
        public ValueItem()
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

        public static readonly DependencyProperty ApplyCommandProperty = DependencyProperty.Register("ApplyCommand", typeof(ICommand), typeof(ValueItem));

        public object ApplyProperty
        {
            get => GetValue(ApplyPropertyProperty);
            set => SetValue(ApplyPropertyProperty, value);
        }

        public static readonly DependencyProperty ApplyPropertyProperty = DependencyProperty.Register("ApplyProperty", typeof(object), typeof(ValueItem));

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public static readonly DependencyProperty DeletePropertyProperty = DependencyProperty.Register("DeleteProperty", typeof(object), typeof(ValueItem));

        public object DeleteProperty
        {
            get => GetValue(DeletePropertyProperty);
            set => SetValue(DeletePropertyProperty, value);
        }

        public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(ValueItem));

        public string ValueName
        {
            get => (string)GetValue(ValueNameProperty);
            set => SetValue(ValueNameProperty, value);
        }

        public static readonly DependencyProperty ValueNameProperty = DependencyProperty.Register("ValueName", typeof(string), typeof(ValueItem));

        public string Expression
        {
            get => (string)GetValue(ExpressionProperty);
            set => SetValue(ExpressionProperty, value);
        }

        public static readonly DependencyProperty ExpressionProperty = DependencyProperty.Register("Expression", typeof(string), typeof(ValueItem));

        public string Answer
        {
            get => (string)GetValue(AnswerProperty);
            set => SetValue(AnswerProperty, value);
        }

        public static readonly DependencyProperty AnswerProperty = DependencyProperty.Register("Answer", typeof(string), typeof(ValueItem));

        public int RoundTo
        {
            get => (int)GetValue(RoundToProperty);
            set => SetValue(RoundToProperty, value);
        }

        public static readonly DependencyProperty RoundToProperty = DependencyProperty.Register("RoundTo", typeof(int), typeof(ValueItem));

        public int ExpThreshold
        {
            get => (int)GetValue(ExpThresholdProperty);
            set => SetValue(ExpThresholdProperty, value);
        }

        public static readonly DependencyProperty ExpThresholdProperty = DependencyProperty.Register("ExpThreshold", typeof(int), typeof(ValueItem));

        public Mode Mode
        {
            get => (Mode)GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(Mode), typeof(ValueItem));

        public bool Expanded
        {
            get => (bool)GetValue(ExpandedProperty);
            set => SetValue(ExpandedProperty, value);
        }

        public static readonly DependencyProperty ExpandedProperty = DependencyProperty.Register("Expanded", typeof(bool), typeof(ValueItem));

        public ICommand NameChanged
        {
            get => (ICommand)GetValue(NameChangedProperty);
            set => SetValue(NameChangedProperty, value);
        }

        public static readonly DependencyProperty NameChangedProperty = DependencyProperty.Register("NameChanged", typeof(ICommand), typeof(ValueItem));

        public object NameChangedProp
        {
            get => GetValue(NameChangedPropProperty);
            set => SetValue(NameChangedPropProperty, value);
        }

        public static readonly DependencyProperty NameChangedPropProperty = DependencyProperty.Register("NameChangedProp", typeof(object), typeof(ValueItem));

        public ICommand ExpressionChanged
        {
            get => (ICommand)GetValue(ExpressionChangedProperty);
            set => SetValue(ExpressionChangedProperty, value);
        }

        public static readonly DependencyProperty ExpressionChangedProperty = DependencyProperty.Register("ExpressionChanged", typeof(ICommand), typeof(ValueItem));

        public object ExpressionChangedProp
        {
            get => GetValue(ExpressionChangedPropProperty);
            set => SetValue(ExpressionChangedPropProperty, value);
        }

        public static readonly DependencyProperty ExpressionChangedPropProperty = DependencyProperty.Register("ExpressionChangedProp", typeof(object), typeof(ValueItem));

        public Error Error
        {
            get => (Error)GetValue(ErrorProperty);
            set => SetValue(ErrorProperty, value);
        }

        public static readonly DependencyProperty ErrorProperty = DependencyProperty.Register("Error", typeof(Error), typeof(ValueItem));

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

        private void Expression_TextChanged(object sender, TextChangedEventArgs e)
        {
            Expression = ((TextBox)sender).Text;
            ExpressionChanged?.Execute(ExpressionChangedProp);
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValueName = ((TextBox)sender).Text;
            NameChanged?.Execute(NameChangedProp);
        }

        private void Name_Loaded(object sender, RoutedEventArgs e)
        {
            name.Focus();
        }

        #endregion
    }
}
