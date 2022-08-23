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
using System.Windows;

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
        public bool PrewActive
        {
            get => (bool)settings["prewActive"];
            set
            {
                if (!value)
                {
                    settings["openMainWindow"] = new KeybindStruct(null, ((KeybindStruct)settings["openMainWindow"]).Modifiers, ((KeybindStruct)settings["openMainWindow"]).VirtualKeyCode);
                    settings["openCalcWindow"] = new KeybindStruct(null, ((KeybindStruct)settings["openCalcWindow"]).Modifiers, ((KeybindStruct)settings["openCalcWindow"]).VirtualKeyCode);
                }

                settings["prewActive"] = value;
                OnPropertyChanged();
                WriteToDB();
                HotKeys.hookManager.UnregisterAll();
                HotKeys.Register();

                OpenMainWindowPrewKeyBindVisibility = Visibility.Visible;
                OpenCalcWindowPrewKeyBindVisibility = Visibility.Visible;
                OpenMainWindowPrewKeyBind = "";
                OpenCalcWindowPrewKeyBind = "";
            }
        }

        public bool AutoRun
        {
            get => (bool)settings["autoRun"];
            set
            {
                settings["autoRun"] = value;
                OnPropertyChanged();
                WriteToDB();

                RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");

                try
                {
                    if (value)
                    {
                        reg.SetValue("FastTool", Directory.GetCurrentDirectory() + "\\FastTool.exe");
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
        public bool Minimize
        {
            get => (bool)settings["minimize"];
            set
            {
                settings["minimize"] = value;
                OnPropertyChanged();
                WriteToDB();
            }
        }

        public string OpenMainWindowMainKeyBind
        {
            get
            {
                string str = "";
                OpenMainWindowPrewKeyBindVisibility = Visibility.Visible;

                try
                {
                    str = ((KeybindStruct)settings["openMainWindow"]).ToString();
                }
                catch
                {
                    return "No";
                }


                return KeyConverter(string.Join(", ", str.Split(", ").Last()));
            }
        }
        public string OpenMainWindowPrewKeyBind
        {
            get
            {
                string str = "";
                try
                {
                    str = ((KeybindStruct)settings["openMainWindow"]).prewkeybind.ToString();
                }
                catch
                {
                    return "No";
                }

                return KeyConverter(string.Join(", ", str.Split(", ").Last()));
            }
            set
            {
                OnPropertyChanged();
            }
        }
        public Visibility OpenMainWindowPrewKeyBindVisibility
        {
            get
            {
                if ((KeybindStruct)settings["openMainWindow"] != null && PrewActive)
                {
                    return Visibility.Visible;
                }

                return Visibility.Collapsed;
            }
            set
            {
                OnPropertyChanged();
            }
        }


        public string OpenCalcWindowMainKeyBind
        {
            get
            {
                string str = "";
                OpenCalcWindowPrewKeyBindVisibility = Visibility.Visible;

                try
                {
                    str = ((KeybindStruct)settings["openCalcWindow"]).ToString();
                }
                catch
                {
                    return "No";
                }

                return string.Join(", ", str.Split(", ").Select(s => KeyConverter(s)));
            }
        }
        public string OpenCalcWindowPrewKeyBind
        {
            get
            {
                string str = "";
                try
                {
                    str = ((KeybindStruct)settings["openCalcWindow"]).prewkeybind.ToString();
                }
                catch
                {
                    return "No";
                }

                return KeyConverter(string.Join(", ", str.Split(", ").Last()));
            }
            set
            {
                OnPropertyChanged();
            }
        }
        public Visibility OpenCalcWindowPrewKeyBindVisibility
        {
            get
            {
                if ((KeybindStruct)settings["openCalcWindow"] != null && PrewActive)
                {
                    return Visibility.Visible;
                }

                return Visibility.Collapsed;
            }
            set
            {
                OnPropertyChanged();
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
                                { "minimize", false },
                                { "prewActive", false },
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

            try { settings["openMainWindow"] = JsonConvert.DeserializeObject<KeybindStruct>(settings["openMainWindow"].ToString()); } catch { settings["openMainWindow"] = null; }
            try { settings["openCalcWindow"] = JsonConvert.DeserializeObject<KeybindStruct>(settings["openCalcWindow"].ToString()); } catch { settings["openCalcWindow"] = null; }

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
                if (keybind.Modifiers.Count == 0 && keybind.VirtualKeyCode == 8)
                {
                    keybind = null;
                }
                currKeybind = keybind;
                if (currKeybindChanging.Contains("Prew"))
                {
                    var mainKeybind = (KeybindStruct)settings[currKeybindChanging.Replace("MainKeyBind", "").Replace("PrewKeyBind", "")];
                    settings[currKeybindChanging.Replace("MainKeyBind", "").Replace("PrewKeyBind", "")] = new KeybindStruct(currKeybind, mainKeybind.Modifiers, mainKeybind.VirtualKeyCode);
                }
                else
                {
                    settings[currKeybindChanging.Replace("MainKeyBind", "").Replace("PrewKeyBind", "")] = currKeybind;
                }
                OnPropertyChanged((currKeybindChanging.First().ToString().ToUpper() + currKeybindChanging.Substring(1)));

                if (keybind == null || currKeybind.VirtualKeyCode != 0 && !IsKeyUp)
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
