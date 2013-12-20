using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Foundation.Configuration;
using Kafala.Query;
using Kafala.Query.Security;
using StructureMap;

namespace Kafala.Web.UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly FoundationKickStart foundationKickStart = new FoundationKickStart();

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
            var config = new FoundationConfigurator
                {

                    Business =
                        {
                            BusinessInvocationLogger =
                                typeof (Kafala.BusinessManagers.SqlProcBusinessManagerInvocationLogger),
                            EmailLogger = typeof (Foundation.Infrastructure.Notifications.EmailLogger)
                        },

                    Persistence =
                        {
                            EntityTypeHolder = typeof (Kafala.Entities.DoNotMap.EntityAssemblyTypeHolder),
                            ConnectionStringKeyName = "Kafaladb"
                        },

                    UseBuseinssManagers = true,
                    UseEmailing = true,
                    UsePresistence = true,
                    UseQueryContainer = true,
                    UseSecurity = true,
                    UseWeb = true,

                    Web =
                        {
                            AuthenticationService = typeof (Kafala.Query.Security.AuthenticationService),
                            DefaultPageTitle = "Kafala Application",
                            ViewModelsAssemblyHookType =
                                typeof (Kafala.Web.ViewModels.Commitment.CommitmentIndexViewModel)
                        }
                };


            FoundationKickStart.Configure(config);
            ObjectFactory.Configure(cfg => new Foundation.Persistence.Configurations.PersistenceConfigurator().Configure(cfg, config));

            ObjectFactory.Configure(cfg => new Foundation.Infrastructure.Configurations.InfrastructureConfigurator().Configure(cfg, config));

            ObjectFactory.Configure(cfg => new Foundation.Web.Configurations.WebConfigurator().Configure(cfg, config));


            AreaRegistration.RegisterAllAreas();

            ObjectFactory.Configure(cfg => cfg.For<ICurrentAuthenticateUser>().Use<CurrentAuthenticateUser>());


            Mapper.Initialize(AutoMapperConfigurations.Configure);

            
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            
        }
    }
}