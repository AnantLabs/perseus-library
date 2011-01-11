using System;
using System.Runtime.InteropServices;

namespace Perseus.Win32 {
    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT { 
        public int Left; 
        public int Top; 
        public int Right; 
        public int Bottom;
    }

    internal class User32 {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetShellWindow();
        
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowRect(IntPtr hwnd, out RECT rc);
    }

}
