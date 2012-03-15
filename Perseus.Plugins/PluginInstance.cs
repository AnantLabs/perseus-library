using System;
using System.Reflection;

namespace Perseus.Plugins {
    public class PluginInstance<T> where T : IPlugin {
        public PluginInstance(T instance, Assembly assembly, string fileName) {            
            this.Instance = instance;
            this.Name = instance.GetType().Name;
            this.FullName = instance.GetType().FullName;
            this.Assembly = assembly;
            this.FileName = fileName;            
        }
        
        public T Instance { get; protected set; }
        public string Name { get; protected set; }
        public string FullName { get; protected set; }
        public Assembly Assembly { get; protected set; }
        public string FileName { get; protected set; }

        public object Invoke(string method, params object[] args) {
            return this.Assembly.GetType(this.FullName).GetMethod(method).Invoke(
                this.Instance, args
            );
        }
    }
}
