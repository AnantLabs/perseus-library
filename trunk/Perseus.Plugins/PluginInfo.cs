using System;
using System.Reflection;

namespace Perseus.Plugins {
    public class PluginInfo {                
        public PluginInfo(string name, string description, string company, string author, string version) {
            this.Name = name;
            this.Description = description;
            this.Company = company;
            this.Authors = author;
            this.Version = version;
        }
        
        public string Name { get; protected set; }            
        public string Description { get; protected set; }
        public string Company { get; protected set; }
        public string Authors { get; protected set; }
        public string Version { get; protected set; }

        public static PluginInfo FromAssembly(Assembly assembly) {
            return PluginInfo.FromAssembly(assembly, string.Empty);
        }
        public static PluginInfo FromAssembly(Assembly assembly, string authors) {
            AssemblyInfo tmp = new AssemblyInfo(assembly);
            return new PluginInfo(tmp.Title, tmp.Description, tmp.Company, authors, tmp.Version);
        }
    }
}
