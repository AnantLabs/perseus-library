using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace Perseus {    
    public static class PInput {
        [DllImport("user32.dll")]
        private static extern short VkKeyScan(char ch);

        public static Key ResolveKey(string key) {
            key = key.Trim().ToLower();

            if (key == string.Empty) {
                return Key.None;
            }

            if (key.Length == 1) {
                try {                    
                    return KeyInterop.KeyFromVirtualKey(VkKeyScan(key[0]));
                }
                catch {
                    return Key.None;
                }
            }
            
            if (key.StartsWith("num ")) {
                string tmp = key.Substring(4).Trim();
                if (tmp.Length == 1 && PString.IsNumber(tmp[0])) {                    
                    return (Key)Enum.Parse(typeof(Key), "numpad" + tmp, true);
                }
                
                switch (tmp) {
                    case "/":
                        return Key.Divide;                            
                    case "*":
                        return Key.Multiply;                            
                    case "-":
                        return Key.Subtract;                            
                    case "+":
                        return Key.Add;                            
                    case ".":
                        return Key.Decimal;                            
                }
            }

            switch (key) {
                case "alt":
                    return Key.LeftAlt;
                case "ctrl":
                case "control":
                    return Key.LeftCtrl;
                case "shift":
                    return Key.LeftShift;
                case "win":
                case "windows":
                    return Key.LWin;
                case "esc":
                    return Key.Escape;
                case "backspace":
                    return Key.Back;
                case "del":
                    return Key.Delete;
                case "page up":
                case "prior":
                    return Key.PageUp;
                case "page down":
                case "next":
                    return Key.PageDown;
            }


            Key result;
            try {
                result = (Key)Enum.Parse(typeof(Key), key, true);
            }
            catch {
                result = Key.None;
            }

            return result;
        }
    }
}
