using System.Transactions;
using Castle.DynamicProxy;
using NHibernate;

namespace Foundation.Infrastructure.BL
{
    class TransactionInterceptor<T> : BusinessManagerInterceptor<T> where T : class, IBusinessManager
    {
        private readonly ISession session;

        public TransactionInterceptor(IBusinessManagerInvocationLogger businessManagerInvocationLogger, ISession session) : base(businessManagerInvocationLogger, session)
        {
            this.session = session;
        }

        public override void Intercept(IInvocation invocation)
        {
            var invocationId = LogStartOfInvocation(invocation);
            using (var tx = new TransactionScope(TransactionScopeOption.Required))
            {
                using (var transaction = this.session.BeginTransaction())
                {
                    invocation.Proceed();
                    this.session.Transaction.Commit();
                }
                LogEndOfInvocation(invocationId, invocation);
                tx.Complete();
            }
        }
    }
}