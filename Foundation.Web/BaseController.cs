using System;
using System.Globalization;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using StructureMap;

namespace Foundation.Web
{
    public abstract class BaseController : Controller
    {
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
