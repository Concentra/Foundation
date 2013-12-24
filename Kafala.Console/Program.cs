
using Foundation.Infrastructure;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Configurations;
using Foundation.Infrastructure.Notifications;
using Foundation.Persistence;
using Foundation.Persistence.Configurations;
using Foundation.Web;
using Kafala.BusinessManagers;
using Kafala.BusinessManagers.Donor;
using Kafala.Entities.DoNotMap;
using NHibernate;
using StructureMap;

namespace Kafala.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            NHibernateConfigurationFactory.ExportSchema = true;
            NHibernateConfigurationFactory.ExportedSchemaLocation = "KafalaDB.sql";
            var container = ConfigureDependencies();

            var session = container.GetInstance<ISession>();

           var businessManagerContainer = container.GetInstance<IBusinessManagerContainer>();

            var donorBusinessManager = businessManagerContainer.Get<DonorBusinessManager>();

            //donorBusinessManager.Add("Abdo", "1234", null, null);
        }

        private static IContainer ConfigureDependencies()
        {
            var container = new Container(x =>x.AddRegistry(new PersistenceRegistery()));

            container.Configure(x => x.For<IBusinessManagerContainer>().Use<BusinessManagerContainer>());

            container.Configure(x => x.For<IBusinessManagerInvocationLogger>().Singleton().Use<SqlProcBusinessManagerInvocationLogger>());
            container.Configure(x => x.For<IBusinessManagerRegistery>().Use<BusinessManagerRegistery>());
            
            container.Configure(x => x.For<ITypeHolder>().Use<EntityAssemblyTypeHolder>());

            container.Configure(x => x.For<IConnectionString>().Use(new ConnectionString("KafalaDB")));
            container.Configure(x => x.For<IFlashMessenger>().Use<SwallowFlashMessneger>());
            return container;
        }
    }
}
