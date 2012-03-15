using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Perseus;

namespace Perseus.Plugins {
    public class PluginService<T> where T : IPlugin {
        private bool _IsLoaded;

        public PluginService() 
            : this("plugins") { }
        public PluginService(string pluginDirectory) {
            if (!Path.IsPathRooted(pluginDirectory)) {
                pluginDirectory = AppDomain.CurrentDomain.BaseDirectory + pluginDirectory;
            }
            
            // Ensure ending \
            if (!pluginDirectory.EndsWith(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal)) {
                pluginDirectory += Path.DirectorySeparatorChar;
            }

            this.PluginDirectory = pluginDirectory;
            this._IsLoaded = false;
        }

        public void LoadPlugins() {
            if (this._IsLoaded) {
                throw new Exception("Plugins already loaded.");
            }

            this._IsLoaded = true;
            this.Plugins = new List<PluginInstance<T>>();

            if (!Directory.Exists(this.PluginDirectory)) {
                return;
            }

            foreach (string file in Directory.GetFiles(this.PluginDirectory)) {
                if (Path.GetExtension(file) == ".dll") {
                    string name = Path.GetFileNameWithoutExtension(file);
                    PluginEventArgs args = new PluginEventArgs(name);
                    this.OnAssemblyLoading(this, args);
                    if (args.Cancel) {
                        continue;
                    }


                    Assembly pluginAssembly = Assembly.LoadFrom(file);

                    //Go through all types found in the assembly
                    foreach (Type pluginType in pluginAssembly.GetTypes()) {
                        // We can only use public and non abstract types
                        if (pluginType.IsPublic && !pluginType.IsAbstract) {
                            if (pluginType.GetInterface("Perseus.Plugins.IPlugin", false) != null) {                                
                                args = new PluginEventArgs(pluginType.FullName);
                                this.OnPluginFound(this, args);
                                if (args.Cancel) {
                                    continue;
                                }

                                object instance = Activator.CreateInstance(
                                    pluginAssembly.GetType(pluginType.ToString())
                                );
                                if (instance is T) {
                                    PluginInstance<T> plugin = new PluginInstance<T>(
                                        (T)instance,
                                        pluginAssembly,
                                        file
                                    );

                                    this.Plugins.Add(plugin);
                                }
                            }
                        }
                    }
                }
            }
        }

        #region Events
        public event PluginEventHandler PluginFound;
        protected virtual void OnPluginFound(object sender, PluginEventArgs e) {
            if (this.PluginFound != null) {
                this.PluginFound(sender, e);
            }
        }

        public event PluginEventHandler AssemblyLoading;
        protected virtual void OnAssemblyLoading(object sender, PluginEventArgs e) {
            if (this.AssemblyLoading != null) {
                this.AssemblyLoading(sender, e);
            }
        }
        #endregion

        public PluginInstance<T> this[string name] {
            get {
                // In situations where the instance class is the same as the last 
                // namespace we will not require the last namespace.
                int pos = name.LastIndexOf('.');
                string last = string.Empty;
                if (pos >= 0) {
                    last = name.Substring(pos);
                    if (name.EndsWith(last + last)) {
                        name = name.Substring(0, pos);
                    }
                }

                var plugin = from p in this.Plugins                             
                             where p.FullName.Is(name) || p.FullName.Is(name + last) || p.Name.Is(name) || p.Instance.PluginInfo.Name.Is(name)
                             select p;

                if (plugin.Count() > 0) {
                    return plugin.First();
                }

                return null;
            }
        }
        public PluginInstance<T> this[string name, Type type] {
            get {
                var p = this[name];
                
                if (p != null && type.IsInstanceOfType(p.Instance)) {
                    return p;
                }
                
                return null;
            }
        }

        public string PluginDirectory { get; protected set; }
        public List<PluginInstance<T>> Plugins { get; protected set; }
    }
}
