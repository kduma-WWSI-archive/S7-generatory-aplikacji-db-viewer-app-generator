using System;

namespace SqlGenerator
{
    public class Column
    {
        public Column(string name, Table table, string agregate)
        {
            Name = name;
            Table = table;
            Agregate = agregate;
        }

        public string Name { get; private set; }
        public Table Table { get; private set; }
        
        public string Agregate { get; private set; }


        public override string ToString()
        {
            return string.IsNullOrEmpty(Agregate) 
                ? string.Format("{0}.{1}", Table, Name) 
                : string.Format("{0}({1}.{2})", Agregate, Table, Name);
        }
    }
}