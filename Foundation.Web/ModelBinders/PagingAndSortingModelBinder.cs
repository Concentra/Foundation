using System.Web.Mvc;
using Foundation.Web.Paging;

namespace Foundation.Web.ModelBinders
{
    /// <summary>
    /// initializing the pager.
    /// </summary>
   
    public class PagingAndSortingModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            var pagedModel = (PagingAndSortingParameters)model;

            if (pagedModel == null)
            {
                pagedModel = new PagingAndSortingParameters();
            }

            return pagedModel;
        }
    }
}