using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FastTool.WPF.Controls
{
    /// <summary>
    /// Логика взаимодействия для OutputSettings.xaml
    /// </summary>
    public partial class OutputSettings : UserControl
    {
        public OutputSettings()
        {
            InitializeComponent();
            grid.DataContext = this;
        }

        #region Propertys

        public int RoundTo
        {
            get => (int)GetValue(RoundToProperty);
            set => SetValue(RoundToProperty, value);
        }

        public static readonly DependencyProperty RoundToProperty = DependencyProperty.Register("RoundTo", typeof(int), typeof(OutputSettings));

        public int ExpThreshold
        {
            get => (int)GetValue(ExpThresholdProperty);
            set => SetValue(ExpThresholdProperty, value);
        }

        public static readonly DependencyProperty ExpThresholdProperty = DependencyProperty.Register("ExpThreshold", typeof(int), typeof(OutputSettings));

        public ICommand RoundToChangedCommand
        {
            get => (ICommand)GetValue(RoundToChangedCommandProperty);
            set => SetValue(RoundToChangedCommandProperty, value);
        }

        public static readonly DependencyProperty RoundToChangedCommandProperty = DependencyProperty.Register("RoundToChangedCommand", typeof(ICommand), typeof(OutputSettings));

        public ICommand ExpThresholdChangedCommand
        {
            get => (ICommand)GetValue(ExpThresholdChangedCommandProperty);
            set => SetValue(ExpThresholdChangedCommandProperty, value);
        }

        public static readonly DependencyProperty ExpThresholdChangedCommandProperty = DependencyProperty.Register("ExpThresholdChangedCommand", typeof(ICommand), typeof(OutputSettings));

        #endregion

        #region Hendlers

        private void RoundSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RoundToChangedCommand?.Execute(RoundTo);
        }

        private void ExpThresholdSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ExpThresholdChangedCommand?.Execute(RoundTo);
        }

        #endregion
    }
}
