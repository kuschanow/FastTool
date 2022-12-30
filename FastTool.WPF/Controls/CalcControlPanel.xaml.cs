#nullable disable
using FastTool.CalculationTool;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для CalcControlPanel.xaml
    /// </summary>
    public partial class CalcControlPanel : UserControl
    {
        public CalcControlPanel()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region Propertys

        public Mode Mode
        {
            get => (Mode)GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(Mode), typeof(CalcControlPanel));

        public bool OutputScreen
        {
            get => (bool)GetValue(OutputScreenProperty);
            set => SetValue(OutputScreenProperty, value);
        }

        public static readonly DependencyProperty OutputScreenProperty = DependencyProperty.Register("OutputScreen", typeof(bool), typeof(CalcControlPanel));

        public int RoundTo
        {
            get => (int)GetValue(RoundToProperty);
            set => SetValue(RoundToProperty, value);
        }

        public static readonly DependencyProperty RoundToProperty = DependencyProperty.Register("RoundTo", typeof(int), typeof(CalcControlPanel));

        public int ExpThreshold
        {
            get => (int)GetValue(ExpThresholdProperty);
            set => SetValue(ExpThresholdProperty, value);
        }

        public static readonly DependencyProperty ExpThresholdProperty = DependencyProperty.Register("ExpThreshold", typeof(int), typeof(CalcControlPanel));

        public ICommand ModeSwitchCommand
        {
            get => (ICommand)GetValue(ModeSwitchCommandProperty);
            set => SetValue(ModeSwitchCommandProperty, value);
        }

        public static readonly DependencyProperty ModeSwitchCommandProperty = DependencyProperty.Register("ModeSwitchCommand", typeof(ICommand), typeof(CalcControlPanel));

        public ICommand MemorySaveCommand
        {
            get => (ICommand)GetValue(MemorySaveCommandProperty);
            set => SetValue(MemorySaveCommandProperty, value);
        }

        public static readonly DependencyProperty MemorySaveCommandProperty = DependencyProperty.Register("MemorySaveCommand", typeof(ICommand), typeof(CalcControlPanel));

        public ICommand HistoryClearCommand
        {
            get => (ICommand)GetValue(HistoryClearCommandProperty);
            set => SetValue(HistoryClearCommandProperty, value);
        }

        public static readonly DependencyProperty HistoryClearCommandProperty = DependencyProperty.Register("HistoryClearCommand", typeof(ICommand), typeof(CalcControlPanel));

        public ICommand RoundToChangedCommand
        {
            get => (ICommand)GetValue(RoundToChangedCommandProperty);
            set => SetValue(RoundToChangedCommandProperty, value);
        }

        public static readonly DependencyProperty RoundToChangedCommandProperty = DependencyProperty.Register("RoundToChangedCommand", typeof(ICommand), typeof(CalcControlPanel));

        public ICommand ExpThresholdChangedCommand
        {
            get => (ICommand)GetValue(ExpThresholdChangedCommandProperty);
            set => SetValue(ExpThresholdChangedCommandProperty, value);
        }

        public static readonly DependencyProperty ExpThresholdChangedCommandProperty = DependencyProperty.Register("ExpThresholdChangedCommand", typeof(ICommand), typeof(CalcControlPanel));

        #endregion

        #region Hendlers

        private void ModeSwitchButton_Click(object sender, RoutedEventArgs e)
        {
            Mode = (Mode)(((int)Mode + 1) % 3);
            ModeSwitchCommand?.Execute(Mode);
        }

        private void MSButton_Click(object sender, RoutedEventArgs e)
        {
            MemorySaveCommand?.Execute(null);
        }

        private void HCButton_Click(object sender, RoutedEventArgs e)
        {
            HistoryClearCommand?.Execute(null);
        }
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
