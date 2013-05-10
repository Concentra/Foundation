using System;
using System.Linq;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Foundation.Infrastructure.BL
{
    public class BusinessManagerRegisterationConventrion : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if(type.GetInterfaces().Contains(typeof(IBusinessManager)) && !type.IsAbstract)
            {
                registry.For(typeof (IBusinessManager)).Use(type);
            }
        }
    }
}