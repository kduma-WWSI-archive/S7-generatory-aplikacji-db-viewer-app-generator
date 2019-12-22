using System;

namespace Generator.PlugIn
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PluginAttribute : System.Attribute
    {
        public PluginAttribute() { }

        public string Name { get; set; }
    }
}