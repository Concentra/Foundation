using System;
using System.Reflection;
using System.Web.Mvc;
using StructureMap;
using Foundation.Web;
using Kafala.Web.UI.Resources;
using Kafala.Query.Security;
using Foundation.Web.Security;
using StructureMap.Configuration.DSL;
using Foundation.Infrastructure.Notifications;

namespace Kafala.Web.UI
{
    public class WebRegistery : Registry, IWebRegistery
    {
        public WebRegistery()
        {
            this.For<IFormAuthenticationService>().Use<DefaultFormAuthenticationService>();
            this.For<IPasswordEncoder>().Use<Base64Encoder>();
            this.For<IPasswordHelper>().Use<PasswordHelper>();
            this.For<IUserAuthenticationFacade>().Use<UserAuthenticationFacade>();
            this.For<IEmailService>().Use<EmailService>();
            this.For<IResourcesLocator>().Use<Kafala.Web.UI.ResourcesLocator>();
            this.Scan(x =>
            {
                x.Assembly(Assembly.GetExecutingAssembly().GetName().Name);
                x.With(new ControllerRegistrationConvention());
            });

            
        }
    }
}
