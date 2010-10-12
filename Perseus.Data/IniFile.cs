using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

using Perseus;

namespace Perseus.Data {
    public class IniFile : Dictionary<string, Dictionary<string, string>>, ICloneable {
        public IniFile() {
            this.FileName = string.Empty;
        }
        public IniFile(string fileName) {
            this.FileName = fileName;
            this.Load(fileName);
        }

        public string FileName { get; set; }

        public void Load() {
            this.Load(this.FileName, false);
        }
        public void Load(bool append) {
            this.Load(this.FileName, append);
        }
        public void Load(string fileName) {
            this.Load(fileName, false);
        }
        public void Load(string fileName, bool append) {
            if (!append) {
                this.Clear();
            }

            if (!File.Exists(fileName)) { return; }

            using (StreamReader sr = new StreamReader(fileName)) {
                this.Load(sr);
            }
        }
        private void Load(StreamReader stream) {
            string section = string.Empty;

            while (!stream.EndOfStream) {
                string line = stream.ReadLine().Trim();
                if (line.Length > 0) {
                    if (line.EnclosedWith("[", "]")) {
                        section = line.Substring(1, line.Length - 2).Trim();
                    }
                    else {
                        if (!this.ContainsKey(section)) {
                            this[section] = new Dictionary<string, string>();
                        }

                        string[] s = line.Split(new string[] { "=" }, 2, StringSplitOptions.None);
                        if (s.Length == 2) {
                            this[section][s[0].Trim()] = s[1].Trim();
                        }
                        else {
                            this[section][s[0].Trim()] = string.Empty;
                        }
                    }
                }
            }
        }
        public void Save() {
            this.Save(this.FileName);
        }
        public void Save(string fileName) {
            if (fileName != string.Empty) {
                using (StreamWriter sw = new StreamWriter(fileName)) {
                    foreach (string section in this.Keys) {
                        sw.WriteLine("[" + section + "]");
                        foreach (string key in this[section].Keys) {
                            sw.WriteLine(key + " = " + this[section][key]);
                        }
                        sw.WriteLine();
                    }
                }
            }
        }

        public string GetString(string group, string key) {
            return this.GetString(group, key, string.Empty);
        }
        public string GetString(string group, string key, string defaultValue) {
            if (this.ContainsKey(group) && this[group].ContainsKey(key)) {                
                return this[group][key];                
            }

            return defaultValue;
        }

        public int GetInt(string group, string key) {
            return this.GetInt(group, key, 0);
        }
        public int GetInt (string group, string key, int defaultValue) {
            if (this.ContainsKey(group) && this[group].ContainsKey(key)) {
                int value;
                if (int.TryParse(this[group][key], out value)) {
                    return value;
                }
            }

            return defaultValue;
        }
        
        public float GetFloat(string group, string key) {
            return this.GetFloat(group, key, 0);
        }
        public float GetFloat(string group, string key, float defaultValue) {
            if (this.ContainsKey(group) && this[group].ContainsKey(key)) {
                float value;
                if (float.TryParse(this[group][key], out value)) {
                    return value;
                }
            }

            return defaultValue;
        }

        public double GetDouble(string group, string key) {
            return this.GetDouble(group, key, 0);
        }
        public double GetDouble(string group, string key, double defaultValue) {
            if (this.ContainsKey(group) && this[group].ContainsKey(key)) {
                double value;
                if (double.TryParse(this[group][key], out value)) {
                    return value;
                }
            }

            return defaultValue;
        }

        public bool GetBool(string group, string key) {
            return this.GetBool(group, key, false);
        }
        public bool GetBool(string group, string key, bool defaultValue) {
            if (this.ContainsKey(group) && this[group].ContainsKey(key)) {
                if (this[group][key].ToLower() == "true" || this[group][key] == "1") {
                    return true;
                }
                else {
                    return false;
                }
            }

            return defaultValue;
        }

        public void SetString(string group, string key, string value) {
            if (!this.ContainsKey(group)) {
                this.Add(group, new Dictionary<string, string>());
            }

            if (!this[group].ContainsKey(key)) {
                this[group].Add(key, value);
            }
            else {
                this[group][key] = value;
            }
        }
        public void SetInt(string group, string key, int value) {
            this.SetString(group, key, value.ToString());
        }
        public void SetFloat(string group, string key, float value) {
            this.SetString(group, key, value.ToString());
        }
        public void SetDouble(string group, string key, double value) {
            this.SetString(group, key, value.ToString());
        }
        public void SetBool(string group, string key, bool value) {
            string boolValue = "1";
            if (!value) {
                boolValue = "0";
            }

            this.SetString(group, key, boolValue);
        }


        public void SetNewString(string group, string key, string value) {
            if (!this.ContainsKey(group) || !this[group].ContainsKey(key)) {
                this.SetString(group, key, value);
            }
        }
        public void SetNewInt(string group, string key, int value) {
            this.SetNewString(group, key, value.ToString());
        }
        public void SetNewFloat(string group, string key, float value) {
            this.SetNewString(group, key, value.ToString());
        }
        public void SetNewDouble(string group, string key, double value) {
            this.SetNewString(group, key, value.ToString());
        }
        public void SetNewBool(string group, string key, bool value) {
            this.SetNewString(group, key, (value ? "1" : "0"));
        }

        public bool RemoveValue(string group) {
            return this.Remove(group);
        }
        public bool RemoveValue(string group, string key) {
            if (this.ContainsKey(group)) {
                return this[group].Remove(key);
            }

            return false;
        }

        public object Clone() {
            IniFile clone = new IniFile();

            foreach (string k1 in this.Keys) {
                clone[k1] = new Dictionary<string, string>();

                foreach (string k2 in this[k1].Keys) {
                    clone[k1][k2] = this[k1][k2];
                }
            }

            clone.FileName = this.FileName;

            return clone;            
        }

        public static IniFile FromResource(Uri resource) {
            IniFile iniFile = new IniFile();

            using (StreamReader sr = new StreamReader(
                Application.GetResourceStream(resource).Stream
            )) {
                iniFile.Load(sr);
            }

            return iniFile;
        }
        public static IniFile FromEmbeddedResource(string resource) {
            IniFile iniFile = new IniFile();

            using (StreamReader sr = new StreamReader(
                System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    resource
                )
            )) {
                iniFile.Load(sr);
            }

            return iniFile;
        }
    }
}
