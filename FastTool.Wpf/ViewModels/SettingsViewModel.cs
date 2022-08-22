using FastTool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.Win32;
using System.IO;
using FastTool.HotKey;
using System.Windows.Forms;
using System.Windows.Input;
using ModifierKeys = FastTool.HotKey.ModifierKeys;
using System.Threading.Tasks;
using System.Windows.Controls;
using TextBox = System.Windows.Controls.TextBox;

namespace FastTool.WPF
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private Dictionary<string, object> settings;
        private Settings db_settings;

        private KeybindStruct currKeybind;
        private string currKeybindChanging;
        private bool changing;
        private bool writeToDb;


        #region props
        public bool AutoRun
        {
            get => (bool)settings["autoRun"];
            set
            {
                settings["autoRun"] = value;
                WriteToDB();
                OnPropertyChanged();

                RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");

                try
                {
                    if (value)
                    {
                        reg.SetValue("FastTool", Directory.GetCurrentDirectory() + "FastTool.exe");
                    }
                    else
                    {
                        reg.DeleteValue("FastTool");
                    }
                }
                finally
                {
                }
            }
        }
        public string OpenMainWindowMainKeyBind
        {
            get
            {
                string str = ((KeybindStruct)settings["openMainWindow"]).ToString();

                return string.Join(", ", str.Split(", ").Select(s => KeyConverter(s)));
            }
        }

        public string OpenCalcWindowMainKeyBind
        {
            get
            {
                string str = ((KeybindStruct)settings["openCalcWindow"]).ToString();

                return string.Join(", ", str.Split(", ").Select(s => KeyConverter(s)));
            }
        }
        #endregion


        public SettingsViewModel()
        {
            using var db = new DBContext();

            if (db.settings.Where(s => s.inUse).Count() == 0)
            {
                if (db.settings.Where(s => s.name == "default").Count() == 0)
                {
                    db.settings.Add(new Settings()
                    {
                        inUse = true,
                        name = "default",
                        settingsString = JsonConvert.SerializeObject(new Dictionary<string, object>()
                            {
                                { "autoRun", false },
                                { "openMainWindow", new KeybindStruct(null, new ModifierKeys[] { ModifierKeys.LAlt }, 0x51) },
                                { "openCalcWindow", new KeybindStruct(null, new ModifierKeys[] { ModifierKeys.LAlt , ModifierKeys.LCtrl}, 0x43) }
                            })
                    });
                }
                else
                {
                    var db_settings = db.settings.Where(s => s.name == "default").First();
                    db_settings.inUse = true;
                    db.settings.Update(db_settings);
                }
            }

            db.SaveChanges();

            LoadFromDB();

        }


        private string KeyConverter(string str)
        {
            string key = str.Substring(str.LastIndexOf('+') + 1);
            str = str.Replace(key, key == "0" ? "" : ((Keys)Convert.ToInt32(key)).ToString());
            str = str.TrimStart('+');
            return str;
        }

        private void WriteToDB()
        {
            using var db = new DBContext();
            db_settings.settingsString = JsonConvert.SerializeObject(settings);
            db.settings.Update(db_settings);
            db.SaveChanges();
        }

        private void LoadFromDB()
        {
            using var db = new DBContext();

            db_settings = db.settings.Where(s => s.inUse).First();

            settings = JsonConvert.DeserializeObject<Dictionary<string, object>>(db_settings.settingsString);

            settings["openMainWindow"] = JsonConvert.DeserializeObject<KeybindStruct>(settings["openMainWindow"].ToString());
            settings["openCalcWindow"] = JsonConvert.DeserializeObject<KeybindStruct>(settings["openCalcWindow"].ToString());

            db.SaveChanges();
        }

        private void OnKeyPressed(KeybindStruct keybind, bool IsKeyUp)
        {
            if (keybind.Modifiers.Count == 0 && keybind.VirtualKeyCode == 13)
            {
                return;
            }

            if (!changing && !IsKeyUp)
            {
                changing = true;
                writeToDb = false;
            }

            if (changing)
            {
                currKeybind = keybind;
                settings[currKeybindChanging.Replace("MainKeyBind", "").Replace("PrewKeyBind", "")] = currKeybind;
                OnPropertyChanged((currKeybindChanging.First().ToString().ToUpper() + currKeybindChanging.Substring(1)));

                if (currKeybind.VirtualKeyCode != 0 && !IsKeyUp)
                {
                    changing = false;
                    writeToDb = true;
                }
            }
        }

        public ICommand ChangeKeybind => new RelayCommand(ChangeKeybindExecute);
        public ICommand ChangeKeybindEnd => new RelayCommand(ChangeKeybindEndExecute);
        public ICommand ChangePrewKeybind => new RelayCommand(ChangePrewKeybindExecute);
        public ICommand ChangeKeybindAbort => new RelayCommand(ChangeKeybindAbortExecute);

        private void ChangeKeybindAbortExecute(object obj)
        {
            HotKeys.hookManager.KeyPressed -= OnKeyPressed;
            LoadFromDB();
            HotKeys.Register();
            (obj as TextBox).Focus();
        }

        private void ChangeKeybindExecute(object obj)
        {
            HotKeys.hookManager.UnregisterAll();
            currKeybindChanging = (string)obj;
            HotKeys.hookManager.KeyPressed += OnKeyPressed;
        }

        private void ChangeKeybindEndExecute(object obj)
        {
            HotKeys.hookManager.KeyPressed -= OnKeyPressed;
            if (writeToDb)
            {
                WriteToDB();
                writeToDb = false;
            }
            try
            {
                HotKeys.Register();
                (obj as TextBox).Focus();
            }
            catch
            {
            }
        }

        private void ChangePrewKeybindExecute(object obj)
        {
            HotKeys.hookManager.UnregisterAll();
            currKeybindChanging = (string)obj;
            HotKeys.hookManager.KeyPressed += OnKeyPressed;
        }


        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
