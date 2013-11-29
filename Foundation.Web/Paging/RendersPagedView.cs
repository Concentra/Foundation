using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Foundation.Web.Extensions;

namespace Foundation.Web.Paging
{
    public class RendersPagedView : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller.ViewData != null)
            {
                object model = filterContext.Controller.ViewData.Model;
                if (model != null)
                {
                    if (model.GetType().IsSubclassOf(typeof(PagedViewModel)))
                    {
                        var pagedModel = (PagedViewModel)model;
                        
                        if (pagedModel.PagingInfo == null)
                        {
                           pagedModel.PagingInfo = new PagingInfoViewModel();
                        }
                    }
                }
            } 
            
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.Controller.ViewData != null)
            {
                object model = filterContext.Controller.ViewData.Model;
                if (model != null)
                {
                    PagedViewModel pagingModel = null;

                    if(model.GetType().IsSubclassOf(typeof(PagedViewModel)))
                    {
                        pagingModel = (PagedViewModel) model;
                    }
                    else
                    {
                        var propertyInfo = model.GetType()
                            .GetProperties()
                            .FirstOrDefault(x => x.PropertyType.IsSubclassOf(typeof (PagedViewModel)));
                        if (propertyInfo != null)
                            pagingModel = (PagedViewModel) propertyInfo.GetValue(model);
                    }

                    if (pagingModel !=null)
                    {
                        var pagedModel = pagingModel;
                        
                        var controllerName = filterContext.RouteData.Values["controller"].ToString();
                        var actionName = filterContext.RouteData.Values["action"].ToString();
                        var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

                        Func<object, string> actionFunction = x => urlHelper.Action(actionName, controllerName, x, true);

                        if (pagedModel.PagingInfo != null)
                        {
                            pagedModel.PagingInfo.ActionFunc = actionFunction;
                        }
                    }
                }
            }

            base.OnResultExecuting(filterContext);
        }
    }
}
