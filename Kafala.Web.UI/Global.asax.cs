using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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
using Kafala.Web.ViewModels.Donor;
using StructureMap;

namespace Kafala.Web.UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("content/{*pathInfo}");
            routes.IgnoreRoute("scripts/{*pathInfo}");
            routes.IgnoreRoute("elmah.axd");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            ObjectFactory.Configure(ConfigureDependencies);
            AreaRegistration.RegisterAllAreas();

            foreach (var keyValuePair in GetModels())
            {
                ModelBinders.Binders.Add(keyValuePair);
            }
            
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
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

        public IEnumerable<KeyValuePair<Type, IModelBinder>> GetModels()
        {
            return Assembly.Load("Kafala.Web.ViewModels").GetTypes()
                .Where(x => x.IsSubclassOf(typeof (PagedViewModel)))
                .Select(x => new KeyValuePair<Type, IModelBinder>(x, new PagingInfoModelBinder()));

        }
    }
}