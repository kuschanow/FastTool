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
            grid.DataContext = this;
        }

        #region Propertys

        public float T
        {
            get => (float)GetValue(TProperty);
            set => SetValue(TProperty, value);
        }

        public static readonly DependencyProperty TProperty = DependencyProperty.Register("T", typeof(float), typeof(Timer));

        public TimeSpan Time
        {
            get => (TimeSpan)GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }

        public static readonly DependencyProperty TimeProperty = DependencyProperty.Register("Time", typeof(TimeSpan), typeof(Timer));

        public TimeSpan LeftTime
        {
            get => (TimeSpan)GetValue(LeftTimeProperty);
            set => SetValue(LeftTimeProperty, value);
        }

        public static readonly DependencyProperty LeftTimeProperty = DependencyProperty.Register("LeftTime", typeof(TimeSpan), typeof(Timer));

        public DateTimeOffset? EndTime
        {
            get => (DateTimeOffset?)GetValue(EndTimeProperty);
            set => SetValue(EndTimeProperty, value);
        }

        public static readonly DependencyProperty EndTimeProperty = DependencyProperty.Register("EndTime", typeof(DateTimeOffset?), typeof(Timer));

        public ICommand StartCommand
        {
            get => (ICommand)GetValue(StartCommandProperty);
            set => SetValue(StartCommandProperty, value);
        }

        public static readonly DependencyProperty StartCommandProperty = DependencyProperty.Register("StartCommand", typeof(ICommand), typeof(Timer));

        public ICommand PauseCommand
        {
            get => (ICommand)GetValue(PauseCommandProperty);
            set => SetValue(PauseCommandProperty, value);
        }

        public static readonly DependencyProperty PauseCommandProperty = DependencyProperty.Register("PauseCommand", typeof(ICommand), typeof(Timer));

        public ICommand ResetCommand
        {
            get => (ICommand)GetValue(ResetCommandProperty);
            set => SetValue(ResetCommandProperty, value);
        }

        public static readonly DependencyProperty ResetCommandProperty = DependencyProperty.Register("ResetCommand", typeof(ICommand), typeof(Timer));

        public ICommand RestartCommand
        {
            get => (ICommand)GetValue(RestartCommandProperty);
            set => SetValue(RestartCommandProperty, value);
        }

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

        public object DeleteCommandParametr
        {
            get => GetValue(DeleteCommandParametrProperty);
            set => SetValue(DeleteCommandParametrProperty, value);
        }

        public static readonly DependencyProperty DeleteCommandParametrProperty = DependencyProperty.Register("DeleteCommandParametr", typeof(object), typeof(Timer));

        public string TimerName
        {
            get => (string)GetValue(TimerNameProperty);
            set => SetValue(TimerNameProperty, value);
        }

        public static readonly DependencyProperty TimerNameProperty = DependencyProperty.Register("TimerName", typeof(string), typeof(Timer));

        public TimerAction TimerAction
        {
            get => (TimerAction)GetValue(TimerActionProperty);
            set => SetValue(TimerActionProperty, value);
        }

        public static readonly DependencyProperty TimerActionProperty = DependencyProperty.Register("TimerAction", typeof(TimerAction), typeof(Timer));

        public bool IsEditMode
        {
            get => (bool)GetValue(IsEditModeProperty);
            set => SetValue(IsEditModeProperty, value);
        }

        public static readonly DependencyProperty IsEditModeProperty = DependencyProperty.Register("IsEditMode", typeof(bool), typeof(Timer));

        public bool AutoReset
        {
            get => (bool)GetValue(AutoResetProperty);
            set => SetValue(AutoResetProperty, value);
        }

        public static readonly DependencyProperty AutoResetProperty = DependencyProperty.Register("AutoReset", typeof(bool), typeof(Timer));

        public bool IsStarted
        {
            get => (bool)GetValue(IsStartedProperty);
            set => SetValue(IsStartedProperty, value);
        }

        public static readonly DependencyProperty IsStartedProperty = DependencyProperty.Register("IsStarted", typeof(bool), typeof(Timer));
        
        public bool Resetted
        {
            get => (bool)GetValue(ResettedProperty);
            set => SetValue(ResettedProperty, value);
        }

        public static readonly DependencyProperty ResettedProperty = DependencyProperty.Register("Resetted", typeof(bool), typeof(Timer));

        public bool Started
        {
            get => (bool)GetValue(StartedProperty);
            set => SetValue(StartedProperty, value);
        }

        public static readonly DependencyProperty StartedProperty = DependencyProperty.Register("Started", typeof(bool), typeof(Timer));

        public bool Paused
        {
            get => (bool)GetValue(PausedProperty);
            set => SetValue(PausedProperty, value);
        }

        public static readonly DependencyProperty PausedProperty = DependencyProperty.Register("Paused", typeof(bool), typeof(Timer));

        public int Hours
        {
            get => (int)GetValue(HoursProperty);
            set => SetValue(HoursProperty, value);
        }

        public static readonly DependencyProperty HoursProperty = DependencyProperty.Register("Hours", typeof(int), typeof(Timer));

        public int Minutes
        {
            get => (int)GetValue(MinutesProperty);
            set => SetValue(MinutesProperty, value);
        }

        public static readonly DependencyProperty MinutesProperty = DependencyProperty.Register("Minutes", typeof(int), typeof(Timer));

        public int Seconds
        {
            get => (int)GetValue(SecondsProperty);
            set => SetValue(SecondsProperty, value);
        }

        public static readonly DependencyProperty SecondsProperty = DependencyProperty.Register("Seconds", typeof(int), typeof(Timer));


        #endregion

        #region Handlers

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditCommand?.Execute(IsEditMode);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteCommand?.Execute(DeleteCommandParametr);
        }
        private void Piker_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "00")
                ((TextBox)sender).Text = "";
        }
        private void Piker_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Minutes > 59)
                Minutes = 59;
            if (Seconds > 59)
                Seconds = 59;
        }

        #endregion
    }
}
