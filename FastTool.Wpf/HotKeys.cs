using FastTool.HotKey;
using FastTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Linq;

namespace FastTool.WPF
{
    public static class HotKeys
    {
        #region props
        public static KeyboardHookManager hookManager { get; set; } = new KeyboardHookManager();
        public static MainWindow Main { get; set; }
        public static TinyCalculator TinyCalc { get; set; } = new TinyCalculator();
        #endregion

        public static void Start()
        {
            hookManager.Start();
        }

        public static void Stop()
        {
            hookManager.Stop();
        }

        public static void Register()
        {
            using var db = new DBContext();

            var settings = JsonConvert.DeserializeObject<Dictionary<string, object>>(db.settings.Where(s => s.inUse).First().settingsString);

            hookManager.RegisterHotkey(JsonConvert.DeserializeObject<KeybindStruct>(settings["openMainWindow"].ToString()), async () => await OpenMainWindow(Main));

            hookManager.RegisterHotkey(JsonConvert.DeserializeObject<KeybindStruct>(settings["openCalcWindow"].ToString()), async () => await OpenTiniCalc(Main));
        }

        #region functions
        private static async Task OpenMainWindow(MainWindow main)
        {
            await Task.Run(() =>
            {
                main.Dispatcher.Invoke(() =>
                {
                    main.mainWindowViewModel.ChangeWindowVisibility.Execute(null);
                    if (main.Visibility == Visibility.Visible)
                    {
                        main.Activate();
                        main.Topmost = true;
                        main.Topmost = false;
                    }
                });
            });
        }

        private static async Task OpenTiniCalc(MainWindow main)
        {
            await Task.Run(() =>
            {
                main.Dispatcher.Invoke(() =>
                {
                    TinyCalc.Show();
                    TinyCalc.Activate();
                    TinyCalc.expression.Focus();
                });
            });
        }
        #endregion

    }
}
