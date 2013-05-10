using System;
using System.Reflection;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using NHibernate.Cfg;

namespace Foundation.Persistence
{
    public class NHibernateMappingConfigurationFactory : INHibernateMappingConfigurationFactory
    {
        public FluentConfiguration BuildMapping(FluentConfiguration configuration, Type type, string nameSpace)
        {
            configuration = configuration
               .Mappings(m =>
               {
                   m.AutoMappings.Add(
                       AutoMap.Assemblies(Assembly.GetAssembly(type))
                       .UseOverridesFromAssembly(Assembly.GetExecutingAssembly())
                       .Conventions.AddAssembly(Assembly.GetExecutingAssembly())
                       .Where(t => t.Namespace == nameSpace));
                   m.HbmMappings.AddFromAssembly(Assembly.Load("Kafala.Entities")); // for stored procedures
                  
               });

            return configuration;
        }
    }
}