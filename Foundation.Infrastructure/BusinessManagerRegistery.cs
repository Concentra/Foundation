using System;
using System.Reflection;
using Foundation.Infrastructure.BL;
using StructureMap.Configuration.DSL;

namespace Foundation.Infrastructure
{
    public class BusinessManagerRegistery : Registry , IBusinessManagerRegistery
    {
        public BusinessManagerRegistery()
        {
            this.For<IBusinessManagerContainer>().Use<BusinessManagerContainer>();
            var dir = AppDomain.CurrentDomain.BaseDirectory;

            this.Scan(x =>
            {
                x.AssembliesFromPath(dir);
                x.With(new BusinessManagerRegisterationConventrion());
                x.ConnectImplementationsToTypesClosing(typeof(BusinessManagerInterceptor<>));
            });
        }
    }
}
