using System;
using System.Transactions;
using Castle.DynamicProxy;
using NHibernate;
using NHibernate.Transaction;
using Newtonsoft.Json;
using IInterceptor = Castle.DynamicProxy.IInterceptor;

namespace Foundation.Infrastructure.BL
{
    public abstract class BusinessManagerInterceptor<T> : IInterceptor where T : class , IBusinessManager 
    {
        private readonly ISession session;

        protected BusinessManagerInterceptor(IBusinessManagerInvocationLogger businessManagerInvocationLogger,ISession session)
        {
            this.session = session;
            Console.Write("base BusinessManagerInterceptor constructed");
        }

        public Guid LogStartOfInvocation(IInvocation invocation)
        {
           
           var callGuide = Guid.NewGuid();
           var startTime = DateTime.UtcNow;
           var managerType = invocation.TargetType;
           var methodName = invocation.Method.Name;
           var arguments = invocation.Arguments;
           var jsonArguments = JsonConvert.SerializeObject(managerType);
           session.Transaction.Begin();
            return callGuide;
           
        }

        public Guid LogEndOfInvocation(Guid invocationId, IInvocation invocation)
        {
            var endTime = DateTime.UtcNow;
            session.Transaction.Commit();
            return invocationId;
        }

        public abstract void Intercept(IInvocation invocation);
    }
}