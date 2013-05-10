using System;
using NHibernate;

namespace Foundation.Persistence
{
    public class SessionFactoryProvider : ISessionFactoryProvider
    {
        private readonly ISessionFactoryFactory sessionFactoryFactory;
        private static ISessionFactory sessionFactory;
        private static readonly object SyncRoot = new object();

        public SessionFactoryProvider(ISessionFactoryFactory sessionFactoryFactory)
        {
            this.sessionFactoryFactory = sessionFactoryFactory;
        }
        
        public ISessionFactory Get()
        {
            if (sessionFactory == null)
            {
                lock (SyncRoot)
                {
                    if (sessionFactory == null)
                    {
                        sessionFactory = this.sessionFactoryFactory.Create();
                    }
                }
            }

            return sessionFactory;
        }
    }
}