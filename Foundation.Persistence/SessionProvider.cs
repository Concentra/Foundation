using System;
using System.Data;
using NHibernate;

namespace Foundation.Persistence
{
    public class SessionProvider : ISessionProvider
    {
        private readonly ISession session;
        private readonly ITransaction transaction;

        public SessionProvider(ISession session)
        {
            this.session = session;
            this.transaction = this.session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public ISession GetSession()
        {
            return this.session;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing && this.transaction != null && this.transaction.IsActive)
            {
                this.transaction.Commit();
            }
        }
    }
}