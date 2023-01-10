using FastTool.CalculationTool;
using FastTool.WPF.ViewModels.Calculator;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        }

        #region Propertys

        public ICommand ApplyCommand
        {
            get => (ICommand)GetValue(ApplyCommandProperty);
            set => SetValue(ApplyCommandProperty, value);
        }

        public static readonly DependencyProperty ApplyCommandProperty = DependencyProperty.Register("ApplyCommand", typeof(ICommand), typeof(MemoryItem));

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(MemoryItem));

        #endregion

        #region Hendlers

        private void Apply_Click(object sender, RoutedEventArgs e) => ApplyCommand?.Execute(DataContext);

        private void Delete_Click(object sender, RoutedEventArgs e) => DeleteCommand?.Execute(DataContext);

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) => ((MemoryViewModel)DataContext).Expression = ((TextBox)sender).Text;

        #endregion

    }
}
