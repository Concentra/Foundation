using System;
using System.Globalization;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace Foundation.Web
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        private static readonly object NestedContainerKey = new object();
        private readonly IContainer container;

        public StructureMapControllerFactory(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            this.container = container;
        }

        public override void ReleaseController(IController controller)
        {
            var controllerBase = controller as Controller;

            if (controllerBase != null && controllerBase.ControllerContext != null)
            {
                var httpContextBase = controllerBase.ControllerContext.HttpContext;

                var nestedContainer = (IContainer)httpContextBase.Items[NestedContainerKey];

                if (nestedContainer != null)
                {
                    nestedContainer.Dispose();
                }
            }

            base.ReleaseController(controller);
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            var nestedContainer = this.container.GetNestedContainer();
            requestContext.HttpContext.Items[NestedContainerKey] = nestedContainer;

            ControllerBase controllerBase = null;

            Func<ControllerContext> ctxtCtor = () => controllerBase == null ? null : controllerBase.ControllerContext;

            nestedContainer.Configure(cfg =>
            {
                cfg.For<RequestContext>().Use(requestContext);
                cfg.For<HttpContextBase>().Use(requestContext.HttpContext);
                cfg.For<Func<ControllerContext>>().Use(ctxtCtor);
                cfg.For<IFlashMessenger>()
                    .HybridHttpOrThreadLocalScoped()
                    .Use(x =>
                    {
                        var controllerContext = x.GetInstance<Func<ControllerContext>>()();
                        var currentController = controllerContext.Controller;
                        var tempData = currentController.TempData;
                        var resourceLocator = nestedContainer.GetInstance<IResourcesLocator>();
                        var flashMessenger = new WebFlashMessenger(resourceLocator);
                        tempData["FlashMessenger"] = flashMessenger;
                        return flashMessenger;
                    });
            });

            var controller = nestedContainer.TryGetInstance<IController>(controllerName);
            controllerBase = controller as ControllerBase;
            
            if (controller == null)
            {
                throw new HttpException(
                    (int)HttpStatusCode.NotFound,
                    string.Format(CultureInfo.CurrentUICulture, "No controller found for {0} at path {1}.", new object[] { controllerName, requestContext.HttpContext.Request.Path }));
            }

            return controller;
        }
    }
}
