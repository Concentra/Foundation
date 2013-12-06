using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Foundation.Web.Paging;

namespace Foundation.Web.ModelBinders
{
    public class PagingInfoModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            var pagedModel = (PagedViewModel) model;

            if (pagedModel.PagingInfo == null)
            {
                pagedModel.PagingInfo = new PagingInfoViewModel();
            }

            return model;
        }
    }
}
