using Foundation.Configuration;
using Foundation.Configuration.Extensions;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Notifications;
using Foundation.Infrastructure.Query;
using StructureMap;

namespace Foundation.Infrastructure.Configurations
{
    public class InfrastructureConfigurator : IModuleConfigurator
    {

        public void Configure(ConfigurationExpression cfg, IFoundationConfigurator foundationConfigurator)
        {

            if (foundationConfigurator.UseQueryContainer)
            {
                cfg.AddRegistry(new QueryRegistery());
                cfg.For<IQueryRegistery>().Use<QueryRegistery>();
            }

            if (foundationConfigurator.UseBuseinssManagers)
            {
                cfg.AddRegistry(new BusinessManagerRegistery());
                cfg.For<IBusinessManagerRegistery>().Use<BusinessManagerRegistery>();
                cfg.For<IBusinessManagerInvocationLogger>(foundationConfigurator.Business.BusinessInvocationLogger, true);
            }

            if (foundationConfigurator.UseEmailing)
            {
                cfg.AddRegistry(new EmailRegistery());
                cfg.For<IEmailLogger>(foundationConfigurator.Business.EmailLogger);
            }

            cfg.AddRegistry(new SecurityRegistery());
        }

    }
}
