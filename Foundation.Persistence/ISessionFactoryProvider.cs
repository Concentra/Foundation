using NHibernate;

namespace Foundation.Persistence
{
    /// <summary>
    /// handles the locking bit of creating the SessionFactory
    /// </summary>
    public interface ISessionFactoryProvider
    {
        ISessionFactory Get();
    }
}