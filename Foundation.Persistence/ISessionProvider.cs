using NHibernate;

namespace Foundation.Persistence
{
    /// <summary>
    /// Opens Transaction before retrieving the session , comminting on disposal
    /// </summary>
    public interface ISessionProvider
    {
        ISession GetSession();
    }
}