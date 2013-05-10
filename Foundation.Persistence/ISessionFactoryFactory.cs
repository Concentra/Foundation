using NHibernate;

namespace Foundation.Persistence
{
    /// <summary>
    /// Return Nhibernate SesstionFactory Object
    /// </summary>
    public interface ISessionFactoryFactory
    {
        ISessionFactory Create();
    }
}