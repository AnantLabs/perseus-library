using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perseus.Plugins {
    public class PluginEventArgs {
        public PluginEventArgs(string name) {
            this.Name = name;
            this.Cancel = false;
        }

        public string Name { get; protected set; }
        public bool Cancel { get; set; }
    }
}
