using FastTool.HotKey;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace FastTool.WPF
{
    public class HotKeys
    {
        #region props
        private KeyboardHookManager hookManager { get; set; } = new KeyboardHookManager();
        public TinyCalculator TinyCalc { get; set; } = new TinyCalculator();
        #endregion

        public void Start()
        {
            hookManager.Start();
        }

        public void Stop()
        {
            hookManager.Stop();
        }

        public HotKeys(MainWindow main)
        {
            hookManager.RegisterHotkey(ModifierKeys.LCtrl | ModifierKeys.LAlt, 0x43, async () =>
            {
                await Task.Run(() =>
                {
                    main.Dispatcher.Invoke(() =>
                    {
                        OpenTiniCalc();
                    });
                });
            });

            hookManager.RegisterHotkey(ModifierKeys.LCtrl | ModifierKeys.LAlt, 0x51, async () =>
            {
                await Task.Run(() =>
                {
                    main.Dispatcher.Invoke(() =>
                    {
                        OpenMainWindow(main);
                    });
                });
            });

            Start();
        }

        #region functions
        private void OpenMainWindow(MainWindow main)
        {
            main.mainWindowViewModel.ChangeWindowVisibility.Execute(null);
            if (main.Visibility == Visibility.Visible)
            {
                main.Activate();
                main.Topmost = true;
                main.Topmost = false;
            }
        }

        private void OpenTiniCalc()
        {
            TinyCalc.Show();
            TinyCalc.Topmost = true;
            TinyCalc.Activate();
            TinyCalc.expression.Focus();
        }
        #endregion

    }
}
