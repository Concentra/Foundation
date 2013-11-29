using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Foundation.Web.Extensions;
using StructureMap;
using Foundation.Web;
using Kafala.Web.UI.Resources;
using Kafala.Query.Security;
using Foundation.Web.Security;
using StructureMap.Configuration.DSL;
using Foundation.Infrastructure.Notifications;
using StructureMap.Pipeline;

namespace Kafala.Web.UI
{
    public class WebRegistery : Registry, IWebRegistery
    {
        public WebRegistery()
        {
            this.For<IPasswordHelper>().Use<PasswordHelper>();
            this.For<IAuthenticationService>().Use<AuthenticationService>();
            this.For<ICurrentAuthenticateUser>().Use<CurrentAuthenticateUser>();
            this.For<IEmailService>().Use<EmailService>();
            this.For<IEmailLogger>().Use<EmailLogger>();
            this.For<IResourcesLocator>().Use<ResourcesLocator>();
<<<<<<< HEAD
            this.For<IFlashMessenger>().Use<WebFlashMessenger>();
            this.For<ICacheService>()
           .HybridHttpOrThreadLocalScoped()
           .Use<InMemoryCache>();
=======
>>>>>>> 6b4605e6fcedb1dfd6c3d0f4145eb4a38ac5f9a0
            this.Scan(x =>
            {
                x.Assembly(Assembly.GetExecutingAssembly().GetName().Name);
                x.With(new ControllerRegistrationConvention());
            });
            this.For<HttpSessionStateBase>().Use(ctx => new HttpSessionStateWrapper(HttpContext.Current.Session));

        }
    }
}
