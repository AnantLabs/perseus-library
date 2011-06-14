using System;
using System.Reflection;

namespace Perseus.Plugins {
    public class AssemblyInfo {
        Assembly _Assembly;

        public AssemblyInfo(Assembly assembly) {
            this._Assembly = assembly;
        }
        public string Title {
            get {
                object[] attributes = this._Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length != 0 && ((AssemblyTitleAttribute)attributes[0]).Title != string.Empty) {
                    return ((AssemblyTitleAttribute)attributes[0]).Title;
                }
                
                return System.IO.Path.GetFileNameWithoutExtension(this._Assembly.CodeBase);                 
            }
        }

        public string Description {
            get {
                object[] attributes = this._Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length != 0) {
                    return ((AssemblyDescriptionAttribute)attributes[0]).Description;
                }

                return string.Empty;                 
            }
        }

        public string Configuration {
            get {
                object[] attributes = this._Assembly.GetCustomAttributes(typeof(AssemblyConfigurationAttribute), false);
                if (attributes.Length != 0) {
                    return ((AssemblyConfigurationAttribute)attributes[0]).Configuration;
                }

                return string.Empty;
            }
        }

        public string Company {
            get {
                object[] attributes = this._Assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length != 0) {
                    return ((AssemblyCompanyAttribute)attributes[0]).Company;
                }

                return string.Empty;
            }
        }

        public string Product {
            get {
                object[] attributes = this._Assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length != 0) { 
                    return ((AssemblyProductAttribute)attributes[0]).Product; 
                }

                return string.Empty; 
            }
        }

        public string Copyright {
            get {
                object[] attributes = this._Assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length != 0) {
                    return ((AssemblyCopyrightAttribute)attributes[0]).Copyright; 
                }

                return string.Empty; 
            }
        }

        public string Trademark {
            get {
                object[] attributes = this._Assembly.GetCustomAttributes(typeof(AssemblyTrademarkAttribute), false);
                if (attributes.Length != 0) {
                    return ((AssemblyTrademarkAttribute)attributes[0]).Trademark;
                }

                return string.Empty; 
            }
        }

        public string Culture {
            get {
                object[] attributes = this._Assembly.GetCustomAttributes(typeof(AssemblyCultureAttribute), false);
                if (attributes.Length != 0) { 
                    return ((AssemblyCultureAttribute)attributes[0]).Culture; 
                }

                return string.Empty; 
            }
        }

        public uint AlgorithmId {
            get {
                object[] attributes = this._Assembly.GetCustomAttributes(typeof(AssemblyAlgorithmIdAttribute), false);
                if (attributes.Length != 0) {
                    return ((AssemblyAlgorithmIdAttribute)attributes[0]).AlgorithmId; 
                }

                return 0;
            }
        }

        public bool CLSCompliant {
            get {
                object[] attributes = this._Assembly.GetCustomAttributes(typeof(CLSCompliantAttribute), false);
                if (attributes.Length != 0) {
                    return ((CLSCompliantAttribute)attributes[0]).IsCompliant; 
                }

                return false;
            }
        }

        public string DefaultAlias {
            get {
                object[] attributes = this._Assembly.GetCustomAttributes(typeof(AssemblyDefaultAliasAttribute), false);
                if (attributes.Length != 0) {
                    return ((AssemblyDefaultAliasAttribute)attributes[0]).DefaultAlias; 
                }

                return string.Empty; 
            }
        }

        public bool DelaySign {
            get {
                object[] attributes = this._Assembly.GetCustomAttributes(typeof(AssemblyDelaySignAttribute), false);
                if (attributes.Length != 0) { 
                    return ((AssemblyDelaySignAttribute)attributes[0]).DelaySign; 
                }

                return false;
            }
        }

        public string FileVersion {
            get {
                object[] attributes = this._Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                if (attributes.Length != 0) {
                    return ((AssemblyFileVersionAttribute)attributes[0]).Version; 
                }

                return string.Empty; 
            }
        }

        public int AssemblyFlags {
            get {
                object[] attributes = this._Assembly.GetCustomAttributes(typeof(AssemblyFlagsAttribute), false);
                if (attributes.Length != 0) {
                    return ((AssemblyFlagsAttribute)attributes[0]).AssemblyFlags; 
                }

                return 0; 
            }
        }

        public string KeyFile {
            get {
                object[] attributes = this._Assembly.GetCustomAttributes(typeof(AssemblyKeyFileAttribute), false);
                if (attributes.Length != 0) {
                    return ((AssemblyKeyFileAttribute)attributes[0]).KeyFile; 
                }

                return string.Empty; 
            }
        }

        public string KeyName {
            get {
                object[] attributes = this._Assembly.GetCustomAttributes(typeof(AssemblyKeyNameAttribute), false);
                if (attributes.Length != 0) {
                    return ((AssemblyKeyNameAttribute)attributes[0]).KeyName; 
                }

                return string.Empty; 
            }
        }

        public string Version {
            get {
                return this._Assembly.GetName().Version.ToString();
            }
        }
    }
}
