using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator.PlugIn
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class CompanyInfoAttribute : System.Attribute
    {
        public CompanyInfoAttribute() { }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
