namespace Generator.PlugIn
{
    public interface IRegistrable
    {
        bool IsRegistred { get; } 
        bool IsActive { get; } 
    }
}