using FastTool.WPF.ViewModels.Time;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
    /// Логика взаимодействия для Timer.xaml
    /// </summary>
    public partial class Timer : UserControl
    {
        public Timer()
        {
            InitializeComponent();
        }

        #region Propertys

        public static readonly DependencyProperty RestartCommandProperty = DependencyProperty.Register("RestartCommand", typeof(ICommand), typeof(Timer));

        public ICommand EditCommand
        {
            get => (ICommand)GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }

        public static readonly DependencyProperty EditCommandProperty = DependencyProperty.Register("EditCommand", typeof(ICommand), typeof(Timer));

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(Timer));

        #endregion

        #region Handlers

        private void Delete_Click(object sender, RoutedEventArgs e) => DeleteCommand?.Execute(DataContext);

        private void Piker_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "00")
                ((TextBox)sender).Text = "";
        }
        private void Piker_LostFocus(object sender, RoutedEventArgs e)
        {
            var vm = (TimerViewModel)DataContext;

            if (vm.Minutes > 59)
                vm.Minutes = 59;
            if (vm.Seconds > 59)
                vm.Seconds = 59;
        }

        #endregion
    }
}
