using System;
using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace Foundation.Infrastructure.BL
{
    public abstract class BusinessManagerInterceptor<T> : IInterceptor where T : class , IBusinessManager 
    {
        protected BusinessManagerInterceptor(IBusinessManagerInvocationLogger businessManagerInvocationLogger)
        {
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