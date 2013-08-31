using Castle.DynamicProxy;
using NHibernate;

namespace Foundation.Infrastructure.BL
{
    class GenericInterceptor<T> : BusinessManagerInterceptor<T> where T : class, IBusinessManager
    {
        public GenericInterceptor(IBusinessManagerInvocationLogger businessManagerInvocationLogger, ISession session) : base(businessManagerInvocationLogger, session)
        {
        }

        public override void Intercept(IInvocation invocation)
        {
            LogStartOfInvocation(invocation);
            invocation.Proceed();
            LogStartOfInvocation(invocation);
        }
    }
}