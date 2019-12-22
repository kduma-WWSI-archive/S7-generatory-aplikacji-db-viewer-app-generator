namespace Generator.PlugIn.SqlGenerator
{
    public class Table
    {
        public Table(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}