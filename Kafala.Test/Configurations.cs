﻿using Foundation.Infrastructure;
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
using Kafala.Entities.DoNotMap;
using Kafala.Query.Security;
using StructureMap;

namespace Kafala.Test
{
    public class Configurations
    {
        public static void ConfigureDependencies(ConfigurationExpression cfg)
        {
            cfg.AddRegistry(new PersistenceRegistery());

            cfg.AddRegistry(new QueryRegistery());

            cfg.AddRegistry(new BusinessManagerRegistery());

            cfg.For<IQueryRegistery>().Use<QueryRegistery>();

            cfg.For<IBusinessManagerRegistery>().Use<BusinessManagerRegistery>();

            cfg.For<IBusinessManagerInvocationLogger>().Singleton().Use<SqlProcBusinessManagerInvocationLogger>();

            cfg.For<IDataModelLocator>().Use<DataModelLocator>();

            cfg.For<IConnectionString>().Use(new ConnectionString("KafalaDBTest"));

            cfg.For<IFlashMessenger>().Use<SwallowFlashMessneger>();

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