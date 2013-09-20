using Foundation.Infrastructure;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Query;
using Foundation.Persistence;
using Foundation.Web;
using Kafala.BusinessManagers;
using Kafala.Entities.DoNotMap;
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

            cfg.For<ITypeHolder>().Use<TypeHolder>();

            cfg.For<IConnectionString>().Use(new ConnectionString("KafalaDBTest"));

            cfg.For<IFlashMessenger>().Use<SwallowFlashMessneger>();
        }
    }
}