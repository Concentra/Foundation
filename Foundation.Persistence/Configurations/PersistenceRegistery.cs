using NHibernate;
using StructureMap.Configuration.DSL;

namespace Foundation.Persistence.Configurations
{
    public class PersistenceRegistery : Registry
    {
        public PersistenceRegistery()
        {
            this.For<INHibernateConfigurationFactory>().Singleton().Use<NHibernateConfigurationFactory>();

            this.For<INHibernateMappingConfigurationFactory>().Singleton().Use<NHibernateMappingConfigurationFactory>();

            this.For<ISessionFactoryFactory>().Singleton().Use<SessionFactoryFactory>();

            this.For<ISessionFactoryProvider>().Singleton().Use<SessionFactoryProvider>();

            this.For<ISessionProvider>().HybridHttpOrThreadLocalScoped()
                .Use(c =>
                         {
                             var session = c.GetInstance<ISessionFactoryProvider>().Get().OpenSession();
                             session.FlushMode = FlushMode.Commit;
                             session.CacheMode = CacheMode.Ignore;
                             return new SessionProvider(session);
                         });

            this.For<ISession>().Use(c => c.GetInstance<ISessionProvider>().GetSession());
        }
    }
}
