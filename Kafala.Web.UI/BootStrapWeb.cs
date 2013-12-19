using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using AutoMapper;
using Foundation.Infrastructure;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Extensions;
using Foundation.Infrastructure.Notifications;
using Foundation.Infrastructure.Query;
using Foundation.Persistence;
using Foundation.Web;
using Foundation.Web.ModelBinders;
using Foundation.Web.Paging;
using Foundation.Web.Security;
using Kafala.BusinessManagers;
using Kafala.Entities.DoNotMap;
using Kafala.Query;
using Kafala.Query.Security;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Kafala.Web.UI
{
    public class BootStrapWeb
    {
        public static void ConfigureWebApplication(IFoundationConfigurator foundationConfigurator)
        {
            ObjectFactory.Configure(cfg => BootStrapWeb.ConfigureDependencies(cfg , new FoundationConfigurator()));
            
            RegisterPagingAndSortingModelBinders(foundationConfigurator.ViewModelsAssemblyHookType);

            ObjectFactory.Configure(cfg => cfg.For<ICurrentAuthenticateUser>().Use<CurrentAuthenticateUser>());

            Mapper.Initialize(AutoMapperConfigurations.Configure);

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory(ObjectFactory.Container));
            
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
        }

        private static void RegisterPagingAndSortingModelBinders(Type viewModelsAssemblyHook)
        {
            foreach (var keyValuePair in BootStrapWeb.GetModels(viewModelsAssemblyHook))
            {
                ModelBinders.Binders.Add(keyValuePair);
            }

            ModelBinders.Binders.Add(typeof (PagingAndSortingParameters), new PagingAndSortingModelBinder());
            ModelBinders.Binders.Add(typeof (PagingParameters), new PagingModelBinder());
            ModelBinders.Binders.Add(typeof (SortingParameters), new SortingModelBinder());
        }

        private static void ConfigureDependencies(ConfigurationExpression cfg, IFoundationConfigurator foundationConfigurator)
        {
            cfg.AddRegistry(new PersistenceRegistery());

            cfg.AddRegistry(new WebRegistery());

            cfg.AddRegistry(new QueryRegistery());

            cfg.AddRegistry(new BusinessManagerRegistery());

            cfg.For<IQueryRegistery>().Use<QueryRegistery>();

            cfg.For<IBusinessManagerRegistery>().Use<BusinessManagerRegistery>();

            cfg.For<IFlashMessenger>(foundationConfigurator.FlashMessenger);
            cfg.For<IAuthenticationService>(foundationConfigurator.AuthenticationService);
            cfg.For<IEmailLogger>(foundationConfigurator.EmailLogger);
            cfg.For<IResourcesLocator>(foundationConfigurator.ResourceLocator);
            cfg.For<IBusinessManagerInvocationLogger>(foundationConfigurator.BusinessInvocationLogger, true);
            cfg.For<ITypeHolder>(foundationConfigurator.EntityTypeHolder);
            cfg.For<IConnectionString>().Use(new ConnectionString(foundationConfigurator.ConnectionStringKeyName));

        }

       

        
        private static IEnumerable<KeyValuePair<Type, IModelBinder>> GetModels(Type viewModelsAssemblyHook)
        {
            return Assembly.GetAssembly(viewModelsAssemblyHook).GetTypes()
                           .Where(x => x.IsSubclassOf(typeof (PagedViewModel)))
                           .Select(x => new KeyValuePair<Type, IModelBinder>(x, new PagingInfoModelBinder()));

        }
    }
}