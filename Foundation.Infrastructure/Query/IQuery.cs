namespace Foundation.Infrastructure.Query
{
    public interface IQuery
    {
        
    }
    
    public interface IQuery<in TParameters, out TResult> : IQuery
    {
        TResult Execute(TParameters parameters);
    }
}