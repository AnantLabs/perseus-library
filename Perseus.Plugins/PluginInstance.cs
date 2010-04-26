using System;

namespace Perseus.Plugins {
    public class PluginInstance<T> {
        public PluginInstance() : this(default(T), string.Empty) { }
        public PluginInstance(T instance, string assemblyPath) {
            this.Instance = instance;
            this.AssemblyPath = assemblyPath;
        }

        public T Instance { get; set; }
        public string AssemblyPath { get; set; }
    }
}
