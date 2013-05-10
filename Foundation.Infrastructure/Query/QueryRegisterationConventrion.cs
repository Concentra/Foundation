using System;
using System.Linq;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Foundation.Infrastructure.Query
{
    public class QueryRegisterationConventrion : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if(type.GetInterfaces().Contains(typeof(IQuery)) && !type.IsAbstract)
            {
                registry.For(typeof (IQuery)).Use(type);
            }
        }
    }
}