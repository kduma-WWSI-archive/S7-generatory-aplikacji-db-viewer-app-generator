using System;

namespace SqlGenerator
{
    public class KeyPair
    {
        public KeyPair(Table table, string key, Table foreignTable, string foreignKey)
        {
            Table = table;
            ForeignTable = foreignTable;
            Key = key;
            ForeignKey = foreignKey;
        }

        public Table Table { get; private set; }
        public Table ForeignTable { get; private set; }
        public string ForeignKey { get; private set; }
        public string Key { get; private set; }
    }
}