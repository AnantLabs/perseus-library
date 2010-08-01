using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perseus {
    public static class PEnvironment {
        public static bool IsWindowsXp {
            get {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT && 
                    Environment.OSVersion.Version.Major == 5 && 
                    Environment.OSVersion.Version.Minor == 1
                ) {
                    return true;
                }

                return false;
            }
        }
        public static bool IsWindowsVista {
            get {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT &&
                    Environment.OSVersion.Version.Major == 6
                ) {
                    return true;
                }

                return false;
            }
        }
        public static bool IsWindows7 {
            get {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT &&
                    Environment.OSVersion.Version.Major == 6 &&
                    Environment.OSVersion.Version.Minor == 1
                ) {
                    return true;
                }

                return false;
            }
        }
        public static bool IsWindowsXpOrAbove {
            get {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
                    if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) || 
                        Environment.OSVersion.Version.Major >= 6
                    ) {
                        return true;
                    }   
                }

                return false;
            }
        }
        public static bool IsWindowsVistaOrAbove {
            get {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
                    if (Environment.OSVersion.Version.Major >= 6) {
                        return true;
                    }
                }

                return false;
            }
        }
        public static bool IsWindows7OrAbove {
            get {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
                    if ((Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 1) ||
                        Environment.OSVersion.Version.Major >= 7
                    ) {
                        return true;
                    }
                }

                return false;
            }
        }       
        public static bool IsWindowsXpOrBelow {
            get {
                return !PEnvironment.IsWindowsVistaOrAbove;
            }
        }
        public static bool IsWindowsVistaOrBelow {
            get {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
                    if (Environment.OSVersion.Version.Major < 6 || PEnvironment.IsWindowsVista) {
                        return true;
                    }
                }

                return false;
            }
        }
        public static bool IsWindows7OrBelow {
            get {
                if (PEnvironment.IsWindowsVistaOrBelow || PEnvironment.IsWindows7) {
                    return true;
                }

                return false;
            }
        }
    }
}
