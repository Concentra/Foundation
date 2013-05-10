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

            this.Scan(x =>
            {
                x.Assembly(Assembly.GetExecutingAssembly().GetName().Name);
                x.With(new BusinessManagerRegisterationConventrion());
                x.ConnectImplementationsToTypesClosing(typeof(BusinessManagerInterceptor<>));
            });
        }
    }
}
