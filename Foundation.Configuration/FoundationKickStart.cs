using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StructureMap;

namespace Foundation.Configuration
{
    public class FoundationKickStart
    {
        
        public static void Configure(IFoundationConfigurator foundationConfigurator)
        {
            ObjectFactory.Configure(cfg => ConfigureDependencies(cfg , new FoundationConfigurator()));
        }

        private static void ConfigureDependencies(ConfigurationExpression cfg, IFoundationConfigurator foundationConfigurator)
        {
            
        }
       
    }
}