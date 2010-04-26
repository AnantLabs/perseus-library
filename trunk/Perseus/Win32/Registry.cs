using System;
using System.IO;
using Microsoft.Win32;

namespace Perseus.Win32 {
    public static class Registry {
        /// <summary>
        /// Will return a string value of a registry value.
        /// </summary>
        /// <param name="registryHive">The top-level nodes that can exist on a machine.</param>
        /// <param name="subKey">The SubKey where the value you want to return is located.</param>
        /// <param name="name">The name of the value you want to return.</param>
        /// <returns>Returns a registry value of type string.</returns>
        public static string GetString(RegistryHive registryHive, string subKey, string name) {
            return Registry.GetString(registryHive, subKey, name, string.Empty);
        }
        /// <summary>
        /// Will return a string value of a registry value.
        /// </summary>
        /// <param name="registryHive">The top-level nodes that can exist on a machine.</param>
        /// <param name="subKey">The SubKey where the value you want to return is located.</param>
        /// <param name="name">The name of the value you want to return.</param>
        /// <param name="defaultValue">The default value to return if no matching value is found.</param>
        /// <returns>Returns a registry value of type string.</returns>
        public static string GetString(RegistryHive registryHive, string subKey, string name, string defaultValue) {
            return Registry.GetValue(registryHive, subKey, name, defaultValue).ToString();
        }
        public static object GetValue(RegistryHive registryHive, string subKey, string name) {
            return Registry.GetValue(registryHive, subKey, name, null);
        }
        public static object GetValue(RegistryHive registryHive, string subKey, string name, object defaultValue) {
            RegistryKey regKey = null;

            switch (registryHive) {
                case RegistryHive.ClassesRoot:
                    regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(subKey);
                    break;
                case RegistryHive.CurrentConfig:
                    regKey = Microsoft.Win32.Registry.CurrentConfig.OpenSubKey(subKey);
                    break;
                case RegistryHive.CurrentUser:
                    regKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(subKey);
                    break;
                case RegistryHive.DynData:
                    regKey = Microsoft.Win32.Registry.DynData.OpenSubKey(subKey);
                    break;
                case RegistryHive.LocalMachine:
                    regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(subKey);
                    break;
                case RegistryHive.PerformanceData:
                    regKey = Microsoft.Win32.Registry.PerformanceData.OpenSubKey(subKey);
                    break;
                case RegistryHive.Users:
                    regKey = Microsoft.Win32.Registry.Users.OpenSubKey(subKey);
                    break;
            }            

            if (regKey == null) {
                return defaultValue;
            }
            
            object o = (string)regKey.GetValue(name, defaultValue);
            regKey.Close();
            return o;            
        }


        public static string GetMimeType(string fileName) {
            string extension = Path.GetExtension(fileName).ToLower();

            RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(extension);
            if (regKey != null && regKey.GetValue("Content Type") != null) {
                return regKey.GetValue("Content Type").ToString();
            }

            return "application/octetstream";
        }
    }
}
