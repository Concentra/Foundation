using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Foundation.Configuration;
using Foundation.Configuration.Extensions;
using Foundation.Infrastructure.Notifications;
using Foundation.Web.ModelBinders;
using Foundation.Web.Paging;
using Foundation.Web.Security;
using StructureMap;

namespace Foundation.Web.Configurations
{
    public class WebConfigurator : IModuleConfigurator
    {

        public void Configure(ConfigurationExpression cfg, IFoundationConfigurator foundationConfigurator)
        {
            if (foundationConfigurator.UseWeb)
            {
                cfg.AddRegistry(new WebRegistery());
                cfg.For<IResourcesLocator>().Use(foundationConfigurator.Web);
                this.RegisterPagingAndSortingModelBinders(foundationConfigurator.Web.ViewModelsAssemblyHookType);
                ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory(ObjectFactory.Container));
                DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
            }

            if (foundationConfigurator.UseSecurity)
            {
                cfg.For<IAuthenticationService>(foundationConfigurator.Web.AuthenticationService);
                
            }
        }
        
        private void RegisterPagingAndSortingModelBinders(Type viewModelsAssemblyHook)
        {
            foreach (var keyValuePair in GetModels(viewModelsAssemblyHook))
            {
                System.Web.Mvc.ModelBinders.Binders.Add(keyValuePair);
            }

            System.Web.Mvc.ModelBinders.Binders.Add(typeof(PagingAndSortingParameters), new PagingAndSortingModelBinder());
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(PagingParameters), new PagingModelBinder());
            System.Web.Mvc.ModelBinders.Binders.Add(typeof(SortingParameters), new SortingModelBinder());
        }

        private IEnumerable<KeyValuePair<Type, IModelBinder>> GetModels(Type viewModelsAssemblyHook)
        {
            return Assembly.GetAssembly(viewModelsAssemblyHook).GetTypes()
                           .Where(x => x.IsSubclassOf(typeof(PagedViewModel)))
                           .Select(x => new KeyValuePair<Type, IModelBinder>(x, new PagingInfoModelBinder()));
        }

    }
}
