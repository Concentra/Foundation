using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Foundation.Configuration;
using Foundation.Configuration.Extensions;
using StructureMap;

namespace Foundation.Persistence.Configurations
{
    public class PersistenceConfigurator : IModuleConfigurator
    {

        public void Configure(ConfigurationExpression cfg, IFoundationConfigurator foundationConfigurator)
        {
            if (foundationConfigurator.UsePresistence)
            {
                cfg.AddRegistry(new PersistenceRegistery());
                cfg.For<IConnectionString>()
                   .Use(new ConnectionString(foundationConfigurator.Persistence.ConnectionStringKeyName));
            }
        }

    }
}
