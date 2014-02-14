using System.Web.Mvc;
using Foundation.Web.Paging;

namespace Foundation.Web.ModelBinders
{
    public class PagingModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            var pagedModel = (IPagingParameters)model;

            if (pagedModel == null)
            {
                pagedModel = new PagingParameters();
            }

            return pagedModel;
        }
    }
}