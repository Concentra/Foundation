using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Foundation.Infrastructure.Query;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Foundation.Infrastructure.Query
{
    public class QueryContainer : IQueryContainer
    {
        private IContainer nestedContainer;

        public QueryContainer(IContainer container, IQueryRegistery queryRegistery )
        {
            this.nestedContainer = container.GetNestedContainer();
            this.nestedContainer.Configure(x => x.AddRegistry((Registry)queryRegistery));
        }
        
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public T Get<T>() where T : class, IQuery
        {
            var proxyGenerator = new ProxyGenerator();
            
            var proxyGenerationOptions = new ProxyGenerationOptions {Hook = new QueryContainerHook()};

            var constructorArguments = new List<object>();
            var constructor = typeof(T).GetConstructors().FirstOrDefault();
            if (constructor != null)
            {
                var constructorParameters = constructor.GetParameters();
                constructorArguments.AddRange(constructorParameters.Select(parameterInfo => nestedContainer.GetInstance(parameterInfo.ParameterType)));
            }

            var interceptors = nestedContainer.GetAllInstances<QueryInterceptor<T>>().Select(mi => (IInterceptor)mi);
            var objectToReturn = proxyGenerator.CreateClassProxy(typeof(T), constructorArguments.ToArray(), interceptors.ToArray());
            return (T)objectToReturn;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.nestedContainer != null)
                {
                    this.nestedContainer.Dispose();
                    this.nestedContainer = null;
                }
            }
        }
    }

    internal class QueryContainerHook : IProxyGenerationHook
    {
        public void MethodsInspected()
        {
        }

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
        }

        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            return methodInfo.IsPublic;
        }
    }
}
