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

            return model;
        }
    }

    public class PagingModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            var pagedModel = (PagingParameters)model;

            if (pagedModel == null)
            {
                pagedModel = new PagingParameters();
            }

            return model;
        }
    }

    public class SortingModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            var pagedModel = (SortingParameters)model;

            if (pagedModel == null)
            {
                pagedModel = new SortingParameters();
            }

            return model;
        }
    }
}

