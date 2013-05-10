using System;
using NHibernate;

namespace Foundation.Persistence
{
    public class SessionFactoryFactory : ISessionFactoryFactory
    {
        private readonly INHibernateConfigurationFactory configurationFactory;

        public SessionFactoryFactory(INHibernateConfigurationFactory configurationFactory)
        {
            this.configurationFactory = configurationFactory;
        }

        public ISessionFactory Create()
        {
            return this.configurationFactory.Create().BuildSessionFactory();
        }
    }
}