using FastTool.HotKey;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FastTool
{
    [Serializable]
    public class Settings : INotifyPropertyChanged
    {
        private bool autoRun = false;
        private KeybindStruct collapseMainWindow = new KeybindStruct(null, new ModifierKeys[] { ModifierKeys.LCtrl, ModifierKeys.LAlt }, 0x43);
        private KeybindStruct openCalcWindow = new KeybindStruct(null, new ModifierKeys[] { ModifierKeys.LCtrl, ModifierKeys.LAlt }, 0x51);
        private KeybindStruct count = new KeybindStruct(null, new ModifierKeys[] { }, 0xD);
        private KeybindStruct clearExp = new KeybindStruct(null, new ModifierKeys[] { ModifierKeys.Ctrl }, 0x8);
        private KeybindStruct deg = new KeybindStruct(null, new ModifierKeys[] { ModifierKeys.Ctrl }, 0x44);
        private KeybindStruct rad = new KeybindStruct(null, new ModifierKeys[] { ModifierKeys.Ctrl }, 0x52);
        private KeybindStruct grad = new KeybindStruct(null, new ModifierKeys[] { ModifierKeys.Ctrl }, 0x47);

        #region
        public bool AutoRun 
        { 
            get => autoRun;
            set
            {
                autoRun = value;
                OnPropertyChanged();
            }
        }
        public KeybindStruct CollapseMainWindow
        {
            get => collapseMainWindow;
            set
            {
                collapseMainWindow = value;
                OnPropertyChanged();
            }
        }
        public KeybindStruct OpenCalcWindow
        {
            get => openCalcWindow;
            set
            {
                openCalcWindow = value;
                OnPropertyChanged();
            }
        }
        public KeybindStruct Count{
            get => count;
            set
            {
                count = value;
                OnPropertyChanged();
            }
        }
        public KeybindStruct ClearExp{
            get => clearExp;
            set
            {
                clearExp = value;
                OnPropertyChanged();
            }
        }
        public KeybindStruct Deg{
            get => deg;
            set
            {
                deg = value;
                OnPropertyChanged();
            }
        }
        public KeybindStruct Rad{
            get => rad;
            set
            {
                rad = value;
                OnPropertyChanged();
            }
        }
        public KeybindStruct Grad{
            get => grad;
            set
            {
                grad = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
