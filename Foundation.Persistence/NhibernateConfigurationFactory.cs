using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Foundation.Persistence
{
    public class NHibernateConfigurationFactory : INHibernateConfigurationFactory   
    {
        private readonly INHibernateMappingConfigurationFactory mappingConfigurationFactory;
        private readonly IConnectionString connectionString;
        private readonly IDataModelLocator dataModelLocator;

        public static bool ExportSchema { get; set; }

        public static string ExportedSchemaLocation { get; set; }

        public NHibernateConfigurationFactory(
            INHibernateMappingConfigurationFactory mappingConfigurationFactory,
            IConnectionString connectionString,
            IDataModelLocator dataModelLocator)
        {
            this.mappingConfigurationFactory = mappingConfigurationFactory;
            this.connectionString = connectionString;
            this.dataModelLocator = dataModelLocator;
        }
        
        public NHibernate.Cfg.Configuration Create()
        {
            // Configure the database properties
            var configruation = Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2008
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

            this.mappingConfigurationFactory.BuildMapping(configruation, dataModelLocator.HookType, dataModelLocator.NameSpace);

            return configruation.BuildConfiguration();
        }
    }
}
