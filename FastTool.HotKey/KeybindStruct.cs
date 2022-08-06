using System;
using System.Collections.Generic;

namespace FastTool.HotKey
{
    /// <summary>
    /// A struct to represent a keybind (key + modifiers)
    /// When the keybind struct is compared to another keybind struct, the equality is based on the
    /// modifiers and the virtual key code.
    /// </summary>
    public class KeybindStruct : IEquatable<KeybindStruct>
    {
        public readonly int VirtualKeyCode;
        public readonly List<ModifierKeys> Modifiers;
        public readonly Guid? Identifier;
        public readonly KeybindStruct prewkeybind;

        public KeybindStruct(KeybindStruct prewkeybind, IEnumerable<ModifierKeys> modifiers, int virtualKeyCode, Guid? identifier = null)
        {
            this.VirtualKeyCode = virtualKeyCode;
            this.Modifiers = new List<ModifierKeys>(modifiers);
            this.Identifier = identifier;
            this.prewkeybind = prewkeybind;
        }

        public bool Equals(KeybindStruct other)
        {
            if (other == null)
                return false;

            if (this.VirtualKeyCode != other.VirtualKeyCode)
                return false;

            if (this.Modifiers.Count != other.Modifiers.Count)
                return false;

            foreach (var modifier in this.Modifiers)
            {
                if (!other.Modifiers.Contains(modifier))
                {
                    return false;
                }
            }

            if (this.prewkeybind == null && other.prewkeybind == null)
                return true;

            if (this.prewkeybind != null && other.prewkeybind != null)
            {
                if (this.prewkeybind.VirtualKeyCode != other.prewkeybind.VirtualKeyCode)
                    return false;

                if (this.prewkeybind.Modifiers.Count != other.prewkeybind.Modifiers.Count)
                    return false;

                foreach (var modifier in this.prewkeybind.Modifiers)
                {
                    if (!other.prewkeybind.Modifiers.Contains(modifier))
                    {
                        return false;
                    }
                }
            }
            else
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((KeybindStruct)obj);
        }

        public override int GetHashCode()
        {
            var hash = 13;
            hash = (hash * 7) + VirtualKeyCode.GetHashCode();

            // Sum the modifiers separately, so that their order does not affect the final hash
            var modifiersHashSum = 0;
            foreach (var modifier in this.Modifiers)
            {
                modifiersHashSum += modifier.GetHashCode();
            }

            hash = (hash * 7) + modifiersHashSum;

            return hash;
        }
    }
}
