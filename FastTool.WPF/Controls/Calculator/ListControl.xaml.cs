using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FastTool.WPF.Controls
{
    /// <summary>
    /// Логика взаимодействия для ListControl.xaml
    /// </summary>
    public partial class ListControl : UserControl
    {
        public ListControl()
        {
            InitializeComponent();
            grid.DataContext = this;
        }

        #region Propertys

        public ICommand AddCommand
        {
            get => (ICommand)GetValue(AddCommandProperty);
            set => SetValue(AddCommandProperty, value);
        }

        public static readonly DependencyProperty AddCommandProperty = DependencyProperty.Register("AddCommand", typeof(ICommand), typeof(ListControl));

        public ICommand ClearCommand
        {
            get => (ICommand)GetValue(ClearCommandProperty);
            set => SetValue(ClearCommandProperty, value);
        }

        public static readonly DependencyProperty ClearCommandProperty = DependencyProperty.Register("ClearCommand", typeof(ICommand), typeof(ListControl));

        #endregion

        #region Handlers

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddCommand?.Execute(null);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearCommand?.Execute(null);
        }

        #endregion

    }
}
