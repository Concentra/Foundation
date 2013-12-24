using NHibernate.Cfg;

namespace Foundation.Persistence
{
    public interface INHibernateConfigurationFactory
    {
        NHibernate.Cfg.Configuration Create();
    }
}