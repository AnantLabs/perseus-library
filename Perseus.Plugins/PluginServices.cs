using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Perseus;

namespace Perseus.Plugins {
    public class PluginService<T> where T : IPlugin {
        public PluginService() : this("plugins") { }
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
            this.Plugins = new Dictionary<string, PluginInstance<T>>();

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
                                string name = instance.GetType().Name;
                                //Type instanceType = ;
                                //instanceType.Assembly.FullName
                                PluginInstance<T> plugin = new PluginInstance<T>((T)instance, file);
                                this.Plugins.Add(name, plugin);                                
                            }
                        }
                    }
                }
            }
        }

        public PluginInstance<T> this[string name] {
            get {
                if (this.Plugins.ContainsKey(name)) {
                    return this.Plugins[name];
                }                

                return null;
            }
        }

        public string PluginDirectory { get; protected set; }
        public Dictionary<string, PluginInstance<T>> Plugins { get; protected set; }
    }
}
