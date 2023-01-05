using FastTool.CalculationTool;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FastTool.WPF.Controls
{
    /// <summary>
    /// Логика взаимодействия для ModeButton.xaml
    /// </summary>
    public partial class ModeButton : Button
    {
        public ModeButton()
        {
            InitializeComponent();
        }

        public Mode Mode
        {
            get => (Mode)GetValue(ModeProperty);
            set => SetValue(ModeProperty, value);
        }

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(Mode), typeof(ModeButton));

        public ICommand ModeSwitchCommand
        {
            get => (ICommand)GetValue(ModeSwitchCommandProperty);
            set => SetValue(ModeSwitchCommandProperty, value);
        }

        public static readonly DependencyProperty ModeSwitchCommandProperty = DependencyProperty.Register("ModeSwitchCommand", typeof(ICommand), typeof(ModeButton));

        private void ModeSwitchButton_Click(object sender, RoutedEventArgs e)
        {
            Mode = (Mode)(((int)Mode + 1) % 3);
            ModeSwitchCommand?.Execute(Mode);
            Content = Mode.ToString();
        }
    }
}
