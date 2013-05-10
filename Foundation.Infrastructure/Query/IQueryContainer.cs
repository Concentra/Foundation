namespace Foundation.Infrastructure.Query
{
    public interface IQueryContainer
    {
        T Get<T>() where T : class , IQuery;
    }
}