using System.Web.Mvc;
using Foundation.Configuration;
using Foundation.Infrastructure;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Configurations;
using Foundation.Infrastructure.Notifications;
using Foundation.Infrastructure.Query;
using Foundation.Infrastructure.Security;
using Foundation.Persistence;
using Foundation.Persistence.Configurations;
using Foundation.Web;
using Foundation.Web.Security;
using Kafala.BusinessManagers;
using Kafala.Query.Security;
using StructureMap;

namespace Kafala.Test
{
    public class Configurations
    {
        public static void BootStrap()
        {
            var config = new FoundationConfigurator
            {

                Business =
                {
                    BusinessInvocationLogger =
                        typeof(Kafala.BusinessManagers.SqlProcBusinessManagerInvocationLogger),
                    EmailLogger = typeof(Foundation.Infrastructure.Notifications.EmailLogger)
                },

                Persistence =
                {
                    PocoPointer = typeof(Kafala.Entities.Donor),
                    ConnectionStringKeyName = "Kafaladbtest"
                },

                

                UseBuseinssManagers = true,
                UseEmailing = true,
                UsePresistence = true,
                UseQueryContainer = true,
                UseSecurity = true,
                UseWeb = false,
                
            };


            FoundationKickStart.Configure(config);
            ObjectFactory.Configure(cfg => new Foundation.Persistence.Configurations.PersistenceConfigurator().Configure(cfg, config));

            ObjectFactory.Configure(cfg => new Foundation.Infrastructure.Configurations.InfrastructureConfigurator().Configure(cfg, config));

            ObjectFactory.Configure(cfg => new Foundation.Web.Configurations.WebConfigurator().Configure(cfg, config));
            ObjectFactory.Configure(x => x.For<IFlashMessenger>().Use<SwallowFlashMessneger>());
        }
        
        
        public static void ConfigureDependencies(ConfigurationExpression cfg)
        {
            cfg.AddRegistry(new PersistenceRegistery());

            cfg.AddRegistry(new QueryRegistery());

            cfg.AddRegistry(new BusinessManagerRegistery());

            cfg.For<IQueryRegistery>().Use<QueryRegistery>();

            cfg.For<IBusinessManagerRegistery>().Use<BusinessManagerRegistery>();

            cfg.For<IBusinessManagerInvocationLogger>().Singleton().Use<SqlProcBusinessManagerInvocationLogger>();

            cfg.For<IConnectionString>().Use(new ConnectionString("KafalaDBTest"));

            cfg.For<IEmailMessageSender>().Use<SwllowEmailService>();

            cfg.For<IAuthenticationService>().Use<AuthenticationService>();

            cfg.For<IPasswordHelper>().Use<PasswordHelper>();

        }
    }

    public class SwllowEmailService : IEmailMessageSender
    {
        public void Send(string to, string cc, string subject, string body)
        {
            return;
        }
    }
}