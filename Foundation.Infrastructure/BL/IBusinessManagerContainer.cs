namespace Foundation.Infrastructure.BL
{
    public interface IBusinessManagerContainer
    {
        T Get<T>() where T : class , IBusinessManager;
    }
}