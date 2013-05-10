using System;
using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace Foundation.Infrastructure.Query
{
    public abstract class QueryInterceptor<T> : IInterceptor where T : class , IQuery 
    {
        protected QueryInterceptor(IQueryInvocationLogger invocationLogger)
        {
            Console.Write("base QueryInterceptor constructed");
        }

        public Guid LogStartOfInvocation(IInvocation invocation)
        {
           var callGuide = Guid.NewGuid();
           var startTime = DateTime.UtcNow;
           var queryType = invocation.TargetType;
           var methodName = invocation.Method.Name;
           var arguments = invocation.Arguments;
           var jsonArguments = JsonConvert.SerializeObject(arguments);
            return callGuide;
        }

        public Guid LogEndOfInvocation(Guid invocationId, IInvocation invocation)
        {
            var endTime = DateTime.UtcNow;
            return invocationId;
        }

        public abstract void Intercept(IInvocation invocation);
    }
}