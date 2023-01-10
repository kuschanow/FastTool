using FastTool.CalculationTool;
using FastTool.WPF.ViewModels.Calculator;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        }

        #region Propertys

        public ICommand ApplyCommand
        {
            get => (ICommand)GetValue(ApplyCommandProperty);
            set => SetValue(ApplyCommandProperty, value);
        }

        public static readonly DependencyProperty ApplyCommandProperty = DependencyProperty.Register("ApplyCommand", typeof(ICommand), typeof(ValueItem));

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(ValueItem));

        public ICommand NameChangedCommand
        {
            get => (ICommand)GetValue(NameChangedCommandProperty);
            set => SetValue(NameChangedCommandProperty, value);
        }

        public static readonly DependencyProperty NameChangedCommandProperty = DependencyProperty.Register("NameChangedCommand", typeof(ICommand), typeof(ValueItem));

        public ICommand ExpressionChangedCommand
        {
            get => (ICommand)GetValue(ExpressionChangedCommandProperty);
            set => SetValue(ExpressionChangedCommandProperty, value);
        }

        public static readonly DependencyProperty ExpressionChangedCommandProperty = DependencyProperty.Register("ExpressionChangedCommand", typeof(ICommand), typeof(ValueItem));

        #endregion

        #region Hendlers

        private void Apply_Click(object sender, RoutedEventArgs e) => ApplyCommand?.Execute(DataContext);

        private void Delete_Click(object sender, RoutedEventArgs e) => DeleteCommand?.Execute(DataContext);

        private void Expression_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((ValueViewModel)DataContext).Expression = ((TextBox)sender).Text;
            ExpressionChangedCommand?.Execute(DataContext);
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((ValueViewModel)DataContext).Name = ((TextBox)sender).Text;
            NameChangedCommand?.Execute(DataContext);
        }

        private void Name_Loaded(object sender, RoutedEventArgs e) => name.Focus();

        #endregion
    }
}
