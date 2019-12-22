using System.Collections.ObjectModel;
using Generator.PlugIn.SqlGenerator;

namespace Generator.PlugIn
{
    public interface ISqlGenerator
    {
        string GetSql(Collection<Table> tables, Collection<Column> columns, Collection<KeyPair> keyPairs);
    }
}