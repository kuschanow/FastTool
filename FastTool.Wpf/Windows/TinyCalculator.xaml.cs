using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FastTool.WPF
{
    /// <summary>
    /// Логика взаимодействия для TinyCalculator.xaml
    /// </summary>
    public partial class TinyCalculator : Window
    {
        TinyCalculatorViewModel tinyCalculator;

        public TinyCalculator()
        {
            InitializeComponent();

            DataContext = tinyCalculator = new TinyCalculatorViewModel();

            ShowInTaskbar = false;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Hide();
        }

        
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
