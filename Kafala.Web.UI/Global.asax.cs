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
        private readonly BootStrapWeb bootStrapWeb = new BootStrapWeb();

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
            BootStrapWeb.ConfigureWebApplication();
            AreaRegistration.RegisterAllAreas();

            
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            
        }
    }
}