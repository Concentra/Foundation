using NHibernate.Cfg;

namespace Foundation.Persistence
{
    public interface INHibernateConfigurationFactory
    {
        Configuration Create();
    }
}