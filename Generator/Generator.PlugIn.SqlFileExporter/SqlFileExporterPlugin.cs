using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator.PlugIn.SqlFileExporter
{
    [Plugin(Name = "SQL File Exporter")]
    [CompanyInfo(Name = "Krystian Duma", Url = "https://duma.dev/")]
    public class SqlFileExporterPlugin : IExporter
    {
        public string Export(string sql)
        {
            throw new NotImplementedException();
        }
    }
}
