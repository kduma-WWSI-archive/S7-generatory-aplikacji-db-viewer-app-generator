using System;
using System.Collections.ObjectModel;
using System.Linq;
using Generator.PlugIn.SqlGenerator;

namespace Generator.PlugIn.BaseSqlGenerator
{
    [Plugin(Name = "Basic SQL Generator")]
    [CompanyInfo(Name = "Krystian Duma", Url = "https://duma.dev/")]
    public class SqlGeneratorPlugin : ISqlGenerator
    {
        #region ISqlGenerator Members

        public string GetSql(Collection<Table> tables, Collection<Column> columns, Collection<KeyPair> keyPairs)
        {
            var col = string.Join(", ", columns.Select((column => column.ToString())).ToArray());
            var grp = columns.Count(column => !string.IsNullOrEmpty(column.Agregate)) != 0 && keyPairs.Count != 0
                ? string.Join(", ", columns.Where(column => string.IsNullOrEmpty(column.Agregate)).Select(table => table.ToString()).ToArray())
                : "";

            var joi = "";
            var used = new Collection<string>();
            foreach (var pair in keyPairs)
            {
                if (joi == "")
                {
                    joi += pair.Table;
                    used.Add(pair.Table.Name);
                }


                joi += string.Format(" JOIN {0} ON {1}.{2} = {3}.{4}", pair.ForeignTable, pair.Table, pair.ForeignKey,
                    pair.ForeignTable, pair.Key);
                used.Add(pair.ForeignTable.Name);
            }

            var tab = string.Join(", ", tables.Where(table => !used.Contains(table.Name)).Select((table => table.ToString())).ToArray());


            if (joi != "" && tab != "")
            {
                joi = string.Format("{0}, {1}", tab, joi);
            }
            else
            {
                joi = string.Format("{0}{1}", tab, joi);
            }

            if (grp != "")
            {
                grp = string.Format(" GROUP BY {0}", grp);
            }

            return string.Format("SELECT {0} FROM {1}{2}", col, joi, grp);
        }

        #endregion
    }
}