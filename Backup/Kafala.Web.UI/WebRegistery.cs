using System;
using System.Reflection;
using System.Web.Mvc;
using Kafala.BusinessManagers.Security;
using StructureMap;
using Foundation.Web;
using Kafala.Web.UI.Resources;
using Foundation.Web.Security;
using StructureMap.Configuration.DSL;

namespace Kafala.Web.UI
{
    public class WebRegistery : Registry, IWebRegistery
    {
        public WebRegistery()
        {
            this.For<IControllerFactory>().Use<CustomControllerFactory>();
            
            this.For<IFormAuthenticationService>().Use<KafalaFormAuthentication>();

            this.Scan(x =>
            {
                x.Assembly(Assembly.GetExecutingAssembly().GetName().Name);
                x.With(new ControllerRegistrationConvention());
            });
        }
    }
}
