using System;
using System.Reflection;

namespace Perseus.Plugins {
    public class PluginInfo {
        public PluginInfo(string name, string title, string description, string version, string company, string author, string website) {
            this.Name = name;
            this.Title = title;
            this.Description = description;
            this.Version = version;
            this.Company = company;
            this.Author = author;
            this.Website = website;
        }

        public string Name { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public string Version { get; protected set; }
        public string Company { get; protected set; }
        public string Author { get; protected set; }
        public string Website { get; protected set; }

        public static PluginInfo FromAssembly(Assembly assembly) {
            AssemblyInfo tmp = new AssemblyInfo(assembly);
            return new PluginInfo(tmp.Title, tmp.Title, tmp.Description, tmp.Version, tmp.Company, string.Empty, string.Empty);
        }
        public static PluginInfo FromAssembly(string name, Assembly assembly, string author, string website) {
            AssemblyInfo tmp = new AssemblyInfo(assembly);
            return new PluginInfo(name, tmp.Title, tmp.Description, tmp.Version, tmp.Company, author, website);
        }
    }
}
