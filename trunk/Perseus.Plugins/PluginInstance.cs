using System;

namespace Perseus.Plugins {
    public class PluginInstance<T> where T : IPlugin {
        public PluginInstance()
            : this(default(T), string.Empty) { }

        public PluginInstance(T instance, string assemblyPath) {            
            this.Instance = instance;
            this.Name = instance.GetType().Name;
            this.FullName = instance.GetType().FullName;
            this.AssemblyPath = assemblyPath;            
        }
        
        public T Instance { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string AssemblyPath { get; set; }
    }
}
