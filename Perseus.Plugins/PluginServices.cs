using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Perseus;

namespace Perseus.Plugins {
    public class PluginService<T> where T : IPlugin {
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

            this.LoadPlugins();
        }

        private void LoadPlugins() {
            this.Plugins = new List<PluginInstance<T>>();

            if (!Directory.Exists(this.PluginDirectory)) {
                return;
            }

            foreach (string file in Directory.GetFiles(this.PluginDirectory)) {
                if (Path.GetExtension(file) == ".dll") {
                    Assembly pluginAssembly = Assembly.LoadFrom(file);

                    //Go through all types found in the assembly
                    foreach (Type pluginType in pluginAssembly.GetTypes()) {
                        // We can only use public and non abstract types
                        if (pluginType.IsPublic && !pluginType.IsAbstract) {
                            if (pluginType.GetInterface("Perseus.Plugins.IPlugin", false) != null) {                            
                                object instance = Activator.CreateInstance(
                                    pluginAssembly.GetType(pluginType.ToString())
                                );
                                if (instance is T) {
                                    PluginInstance<T> plugin = new PluginInstance<T>(
                                        (T)instance,
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

        public PluginInstance<T> this[string fullName] {
            get {
                var plugin = from p in this.Plugins
                             where p.FullName == fullName
                             select p;

                if (plugin.Count() > 0) {
                    return plugin.First();
                }

                return null;
            }
        }
        public PluginInstance<T> this[Type type, string name] {
            get {
                var plugin = from p in this.Plugins
                             where type.IsInstanceOfType(p.Instance) && p.Name == name
                             select p;

                if (plugin.Count() > 0) {
                    return plugin.First();
                }
                
                return null;
            }
        }

        public string PluginDirectory { get; protected set; }
        public List<PluginInstance<T>> Plugins { get; protected set; }
    }
}
