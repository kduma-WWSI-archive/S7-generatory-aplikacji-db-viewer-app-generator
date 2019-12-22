using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Generator.PlugIn
{
    public class Loader
    {
        public List<LoadedPlugIn> PlugIns { get; private set; }

        public Loader()
        {
            PlugIns = new List<LoadedPlugIn>();
        }

        public void Scan()
        {
            var codeBase = System.Reflection.Assembly.GetEntryAssembly().Location;
            var directoryName = Path.GetDirectoryName(codeBase);
            var files = Directory.GetFiles(directoryName, "*.dll");

            PlugIns.Clear();

            foreach (var file in files)
            {
                var assembly = Assembly.LoadFrom(file);
                var types = assembly.GetTypes().Where(AssemblyFilter).ToArray();

                foreach (var type in types)
                {
                    PlugIns.Add(new LoadedPlugIn(assembly.CreateInstance(type.FullName), assembly.GetName()));
                }
            }
        }

        private bool AssemblyFilter(Type type)
        {
            if (!type.IsClass)
                return false;

            if(!type.GetCustomAttributes(false).Any(o => o is PluginAttribute))
                return false;

            var interfaces = type.GetInterfaces();

            if (interfaces.Any(o => o.FullName == "Generator.PlugIn.ISqlGenerator"))
                return true;

            if (interfaces.Any(o => o.FullName == "Generator.PlugIn.IExporter"))
                return true;

            return false;
        }
    }
}