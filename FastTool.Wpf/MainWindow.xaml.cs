using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FastTool.Models;
using Microsoft.EntityFrameworkCore;
using FastTool.HotKey;
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;

namespace FastTool.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool TrayClose = false;
        public MainWindowViewModel mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();

            mainWindowViewModel = new MainWindowViewModel(this);
            mainWindowViewModel.PropertyChanged += MainWindowViewModel_PropertyChanged;
            DataContext = mainWindowViewModel;
            calcTab.DataContext = mainWindowViewModel.CalcViewModel;

            Hide();
            mainWindowViewModel.ChangeWindowVisibility.Execute(null);

            RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
            try
            {
                reg.SetValue("FastTool", Directory.GetCurrentDirectory() + "FastTool.exe");
            }
            catch
            {

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using var db = new DBContext();
            db.Database.Migrate();
        }

        private void MainWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MainWindowVisibility")
            {
                Visibility = mainWindowViewModel.MainWindowVisibility;
            }
        }

        private void Form_Closing(object sender, CancelEventArgs e)
        {
            if (!TrayClose)
            {
                e.Cancel = true;
                mainWindowViewModel.ChangeWindowVisibility.Execute(mainWindow);
            }
        }

        private void NotifyIcon_DoubleClick(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.ChangeWindowVisibility.Execute(null);
            Activate();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            TrayClose = true;
            Close();
        }

        private void mainWindow_Closed(object sender, EventArgs e)
        {
            foreach (Window w in App.Current.Windows)
                w.Close();
        }
    }
}
