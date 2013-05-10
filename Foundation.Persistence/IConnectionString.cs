namespace Foundation.Persistence
{
    public interface IConnectionString
    {
        string Name { get; }

        string Value { get; }
    }
}
