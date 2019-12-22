using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator.PlugIn.ExeFileExporter
{
    [Plugin(Name = "EXE File Exporter")]
    [CompanyInfo(Name = "Krystian Duma", Url = "https://duma.dev/")]
    public class ExeFileExporterPlugin : IExporter
    {
        public string Export(string sql)
        {
            throw new NotImplementedException();
        }
    }
}
