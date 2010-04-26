using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perseus.Plugins {
    public interface IPlugin {
        PluginInfo PluginInfo { get; }
    }
}
