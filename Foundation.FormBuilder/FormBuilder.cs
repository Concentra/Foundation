using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Foundation.FormBuilder.DynamicForm;

namespace Foundation.FormBuilder
{
    public class FormBuilder<TModel>
    {
        private readonly HtmlHelper<TModel> helper;

        internal FormBuilder(HtmlHelper<TModel> helper)
        {
            this.helper = helper;
        }

        #region view


        private static IDynamicUiBuilder<TModel> viewBuilder;

        private static IDynamicUiBuilder<TModel> GetViewBuilder(HtmlHelper<TModel> helper)
        {
            if (viewBuilder == null)
            {
                viewBuilder = new BootStrapViewBuilder<TModel>();
            }
            return viewBuilder;
        }

        public MvcHtmlString DynamicView(BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            return RenderUI(GetViewBuilder(helper), formType, false);
        }

        #endregion


        #region form

        private static IDynamicUiBuilder<TModel> formBuilder;

        private static IDynamicUiBuilder<TModel> GetFormBuilder()
        {
            if (formBuilder == null)
            {
                formBuilder = new BootStrapFormBuilder<TModel>();
            }
            return formBuilder;
        }

        public MvcHtmlString DynamicForm(BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            return RenderUI(GetFormBuilder(), formType);
        }

        #endregion


        public MvcHtmlString RenderUI(IDynamicUiBuilder<TModel> builder, BootstrapFormType formType, bool renderButtons = true)
        {
            return builder.Build(helper.ViewData.Model, formType, renderButtons, helper);
        }

       

      
    }
}