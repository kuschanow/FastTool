using System;

namespace FastTool.HotKey
{
    [Flags]
    public enum ModifierKeys
    {
        Alt = 1,
        LAlt = 2,
        RAlt = 4,
        Ctrl = 8,
        LCtrl = 16,
        RCtrl = 32,
        Shift = 64,
        LShift = 128,
        RShift = 256,
        LWin = 512,
        RWin = 1024,
    }

    public static class ModifierKeysUtilities
    {
        public static ModifierKeys? GetModifierKeyFromCode(int keyCode)
        {
            switch (keyCode)
            {
                case 0x10:
                    return ModifierKeys.Shift;
                case 0xA0:
                    return ModifierKeys.LShift;
                case 0xA1:
                    return ModifierKeys.RShift;

                case 0x11:
                    return ModifierKeys.Ctrl;
                case 0xA2:
                    return ModifierKeys.LCtrl;
                case 0xA3:
                    return ModifierKeys.RCtrl;

                case 0x12:
                    return ModifierKeys.Alt;
                case 0xA4:
                    return ModifierKeys.LAlt;
                case 0xA5:
                    return ModifierKeys.RAlt;

                case 0x5B:
                    return ModifierKeys.LWin;
                case 0x5C:
                    return ModifierKeys.RWin;

                default:
                    return null;
            }
        }
    }
}