using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FastTool.WPF.Controls.Time
{
    /// <summary>
    /// Логика взаимодействия для TimeController.xaml
    /// </summary>
    public partial class TimeController : UserControl
    {
        public TimeController()
        {
            InitializeComponent();
            grid.DataContext = this;
            Resetted = true;
        }

        #region Propertys

        public TimeSpan Time
        {
            get => (TimeSpan)GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(TimeSpan), typeof(TimeController));

        public bool Paused
        {
            get => (bool)GetValue(PausedProperty);
            set => SetValue(PausedProperty, value);
        }

        public static readonly DependencyProperty PausedProperty = DependencyProperty.Register("Paused", typeof(bool), typeof(TimeController));

        public bool Started
        {
            get => (bool)GetValue(StartedProperty);
            set => SetValue(StartedProperty, value);
        }

        public static readonly DependencyProperty StartedProperty = DependencyProperty.Register("Started", typeof(bool), typeof(TimeController));

        public bool Resetted
        {
            get => (bool)GetValue(ResettedProperty);
            set => SetValue(ResettedProperty, value);
        }

        public static readonly DependencyProperty ResettedProperty = DependencyProperty.Register("Resetted", typeof(bool), typeof(TimeController));

        public ICommand StartCommand
        {
            get => (ICommand)GetValue(StartCommandProperty);
            set => SetValue(StartCommandProperty, value);
        }

        public static readonly DependencyProperty StartCommandProperty = DependencyProperty.Register("StartCommand", typeof(ICommand), typeof(TimeController));

        public ICommand PauseCommand
        {
            get => (ICommand)GetValue(PauseCommandProperty);
            set => SetValue(PauseCommandProperty, value);
        }

        public static readonly DependencyProperty PauseCommandProperty = DependencyProperty.Register("PauseCommand", typeof(ICommand), typeof(TimeController));

        public ICommand ResetCommand
        {
            get => (ICommand)GetValue(ResetCommandProperty);
            set => SetValue(ResetCommandProperty, value);
        }

        public static readonly DependencyProperty ResetCommandProperty = DependencyProperty.Register("ResetCommand", typeof(ICommand), typeof(TimeController));

        public ICommand RestartCommand
        {
            get => (ICommand)GetValue(RestartCommandProperty);
            set => SetValue(RestartCommandProperty, value);
        }

        public static readonly DependencyProperty RestartCommandProperty = DependencyProperty.Register("RestartCommand", typeof(ICommand), typeof(TimeController));

        #endregion

        #region Handlers

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Started = true;
            Paused = false;
            Resetted = false;
            StartCommand?.Execute(null);
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            Started = false;
            Paused = true;
            Resetted = false;
            PauseCommand?.Execute(null);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Started = false;
            Paused = false;
            Resetted = true;
            ResetCommand?.Execute(null);
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            Started = true;
            Paused = false;
            Resetted = false;
            RestartCommand?.Execute(null);
        }

        #endregion
    }
}
