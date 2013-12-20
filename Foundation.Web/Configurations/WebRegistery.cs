using System.Reflection;
using System.Web;
using Foundation.Web.Extensions;
using StructureMap.Configuration.DSL;

namespace Foundation.Web.Configurations
{
    public class WebRegistery : Registry, IWebRegistery
    {
        public WebRegistery()
        {
            
            this.For<ICacheService>()
           .HybridHttpOrThreadLocalScoped()
           .Use<InMemoryCache>();
            this.Scan(x =>
            {
                x.Assembly(Assembly.GetCallingAssembly().GetName().Name);
                x.With(new ControllerRegistrationConvention());
            });
            this.For<HttpSessionStateBase>().Use(ctx => new HttpSessionStateWrapper(HttpContext.Current.Session));
        }
    }
}
