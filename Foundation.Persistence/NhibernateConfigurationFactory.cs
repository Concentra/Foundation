using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Foundation.Configuration;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Foundation.Persistence
{
    public class NHibernateConfigurationFactory : INHibernateConfigurationFactory   
    {
        private readonly INHibernateMappingConfigurationFactory mappingConfigurationFactory;
        private readonly IConnectionString connectionString;
        private readonly IFoundationConfigurator foundationConfigurator;

        public static bool ExportSchema { get; set; }

        public static string ExportedSchemaLocation { get; set; }

        public NHibernateConfigurationFactory(
            INHibernateMappingConfigurationFactory mappingConfigurationFactory,
            IConnectionString connectionString,
            IFoundationConfigurator foundationConfigurator)
        {
            this.mappingConfigurationFactory = mappingConfigurationFactory;
            this.connectionString = connectionString;
            this.foundationConfigurator = foundationConfigurator;
        }
        
        public NHibernate.Cfg.Configuration Create()
        {
            // Configure the database properties
            var configruation = Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2012
                        .ConnectionString(c => c.FromConnectionStringWithKey(this.connectionString.Name))
                );

            // is it required to create schema ?
            if (ExportSchema)
            {
                configruation.ExposeConfiguration(
                    config
                        => new SchemaExport(config)
                        .SetOutputFile(ExportedSchemaLocation)
                        .Create(true, false));    
            }

            this.mappingConfigurationFactory.BuildMapping(configruation,
                foundationConfigurator.Persistence.PocoPointer,
                foundationConfigurator.Persistence.PocoPointer.Namespace);

            return configruation.BuildConfiguration();
        }
    }
}
