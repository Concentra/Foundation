using System;
using System.Globalization;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Foundation.Infrastructure;
using Foundation.Infrastructure.BL;
using Foundation.Infrastructure.Query;
using StructureMap;

namespace Foundation.Web
{
    public class BaseController : Controller
    {
        public IFlashMessenger FlashMessenger { get; set; }
        
        public ICurrentAuthenticateUser CurrentAuthenticateUser { get; set; }
        
        public  BaseController()
        {
            var flashMessenger = new WebFlashMessenger(ObjectFactory.GetInstance<IResourcesLocator>());
            TempData["FlashMessenger"] = flashMessenger;
            this.FlashMessenger = flashMessenger;
        }

        protected ActionResult PageNotFound()
        {
            return this.View("NotFound");
        }

        protected override ViewResult View(string viewName, string masterName, object model)
        {
            if (viewName == null && model != null)
            {
                var modelName = model.GetType().Name;
                viewName = modelName.Replace("ViewModel", string.Empty);
            }

            return base.View(viewName, masterName, model);
        }

        protected override PartialViewResult PartialView(string viewName, object model)
        {
            if (viewName == null && model != null)
            {
                var modelName = model.GetType().Name;
                viewName = string.Format("Partials/_{0}", modelName.Replace("PartialViewModel", string.Empty));
            }

            return base.PartialView(viewName, model);
        }
    }
}
