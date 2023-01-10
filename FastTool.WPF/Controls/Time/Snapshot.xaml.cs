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

namespace FastTool.WPF.Controls.Time
{
    /// <summary>
    /// Логика взаимодействия для Snapshot.xaml
    /// </summary>
    public partial class Snapshot : UserControl
    {
        public Snapshot()
        {
            InitializeComponent();
        }

        #region Propertys

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(TimeController));

        #endregion

        #region Handlers

        private void Delete_Click(object sender, RoutedEventArgs e) => DeleteCommand?.Execute(DataContext);

        #endregion
    }
}
