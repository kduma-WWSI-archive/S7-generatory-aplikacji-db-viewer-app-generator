using System;
using System.Collections.ObjectModel;
using System.Linq;
using Generator.PlugIn;
using Generator.PlugIn.SqlGenerator;

namespace SqlGenerator
{
    public class Generator : ISqlGenerator
    {
        public Generator()
        {
            KeyPairs = new Collection<KeyPair>();
            Columns = new Collection<Column>();
            Tables = new Collection<Table>();
        }

        public Collection<Table> Tables { get; private set; }

        public Collection<Column> Columns { get; private set; }

        public Collection<KeyPair> KeyPairs { get; private set; }

        public override string ToString()
        {
            var col = string.Join(", ", Columns.Select((column => column.ToString())).ToArray());
            var grp = Columns.Count(column => !string.IsNullOrEmpty(column.Agregate)) != 0 && KeyPairs.Count != 0
                ? string.Join(", ", Columns.Where(column => string.IsNullOrEmpty(column.Agregate)).Select(table => table.ToString()).ToArray())
                : "";

            var joi = "";
            var used = new Collection<string>();
            foreach (var pair in KeyPairs)
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

            var tab = string.Join(", ", Tables.Where(table => !used.Contains(table.Name)).Select((table => table.ToString())).ToArray());


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
    }
}