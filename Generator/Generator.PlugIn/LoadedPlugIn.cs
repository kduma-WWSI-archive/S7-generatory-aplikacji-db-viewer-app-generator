using System;
using System.Linq;
using System.Reflection;

namespace Generator.PlugIn
{
    public class LoadedPlugIn
    {
        private readonly object _plugIn;

        private readonly AssemblyName _assemblyName;

        public Version Version { get { return _assemblyName.Version; } }

        public string AssemblyName { get { return _assemblyName.Name; } }

        public bool IsRegistred { get { return true; } }

        public bool IsActive { get { return true; } }

        public bool IsSqlGenerator { get { return _plugIn is ISqlGenerator; } }

        public ISqlGenerator SqlGenerator { get { return _plugIn as ISqlGenerator; } }

        public bool IsExporter { get { return _plugIn is IExporter; } }

        public IExporter SqlExporter { get { return _plugIn as IExporter; } }

        public PluginAttribute Plugin { get{ return _plugIn.GetType().GetCustomAttributes(false).First(t => t is PluginAttribute) as PluginAttribute; } }

        public CompanyInfoAttribute CompanyInfo { get { return _plugIn.GetType().GetCustomAttributes(false).Where(t => t is CompanyInfoAttribute).Cast<CompanyInfoAttribute>().FirstOrDefault(); } }

        public LoadedPlugIn(object plugIn, AssemblyName assemblyName)
        {
            _plugIn = plugIn;
            _assemblyName = assemblyName;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1} @ {2})", Plugin.Name, AssemblyName, Version);
        }
    }
}