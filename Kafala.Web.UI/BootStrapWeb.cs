using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using AutoMapper;
using Foundation.Infrastructure;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Query;
using Foundation.Persistence;
using Foundation.Web;
using Foundation.Web.ModelBinders;
using Foundation.Web.Paging;
using Kafala.BusinessManagers;
using Kafala.Entities.DoNotMap;
using Kafala.Query;
using StructureMap;

namespace Kafala.Web.UI
{
    public class BootStrapWeb
    {
        public static void ConfigureWebApplication()
        {
            ObjectFactory.Configure(BootStrapWeb.ConfigureDependencies);
            foreach (var keyValuePair in BootStrapWeb.GetModels())
            {
                ModelBinders.Binders.Add(keyValuePair);
            }

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory(ObjectFactory.Container));
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
        }
        
        private static void ConfigureDependencies(ConfigurationExpression cfg)
        {
            cfg.AddRegistry(new PersistenceRegistery());

            cfg.AddRegistry(new WebRegistery());

            cfg.AddRegistry(new QueryRegistery());

            cfg.AddRegistry(new BusinessManagerRegistery());

            cfg.For<IQueryRegistery>().Use<QueryRegistery>();

            cfg.For<IBusinessManagerRegistery>().Use<BusinessManagerRegistery>();
            
            cfg.For<IBusinessManagerInvocationLogger>().Singleton().Use<SqlProcBusinessManagerInvocationLogger>();

            cfg.For<ITypeHolder>().Use<TypeHolder>();

            cfg.For<IConnectionString>().Use(new ConnectionString("KafalaDB"));

            Mapper.Initialize(AutoMapperConfigurations.Configure);
        }

        private static IEnumerable<KeyValuePair<Type, IModelBinder>> GetModels()
        {
            return Assembly.Load("Kafala.Web.ViewModels").GetTypes()
                           .Where(x => x.IsSubclassOf(typeof (PagedViewModel)))
                           .Select(x => new KeyValuePair<Type, IModelBinder>(x, new PagingInfoModelBinder()));

        }
    }
}