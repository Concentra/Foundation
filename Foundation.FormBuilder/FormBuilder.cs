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

       

        #region Forms
        #region DynamicView

        static BootstrapDynamicViewBuilder<TModel> defaultDynamicViewBuilder;

        private static BootstrapDynamicViewBuilder<TModel> DefaultDynamicViewBuilder(HtmlHelper<TModel> helper)
        {
            if (defaultDynamicViewBuilder == null)
            {
                defaultDynamicViewBuilder = new BootstrapDynamicViewBuilder<TModel>(helper);
            }
            return defaultDynamicViewBuilder;
        }

        public MvcHtmlString DynamicView(BootstrapFormType formType)
        {
            return DynamicView(DefaultDynamicViewBuilder(helper), BootstrapFormType.Horizontal);
        }

        public MvcHtmlString DynamicView(IDynamicViewBuilder<TModel> builder, BootstrapFormType formType)
        {
            return builder.Build(helper.ViewData.Model, formType);
        }

        #endregion DynamicView


        #region DynamicForm

        private static BootstrapDynamicFormBuilder<TModel> defaultDynamicFormBuilder;

        private static BootstrapDynamicFormBuilder<TModel> DefaultDynamicFormBuilder(HtmlHelper<TModel> helper)
        {
            if (defaultDynamicFormBuilder == null)
            {
                defaultDynamicFormBuilder = new BootstrapDynamicFormBuilder<TModel>(helper);
            }
            return defaultDynamicFormBuilder;
        }

        public MvcHtmlString DynamicForm()
        {
            return DynamicForm(DefaultDynamicFormBuilder(helper), BootstrapFormType.Horizontal);
        }

        public MvcHtmlString DynamicForm(IDynamicFormBuilder<TModel> builder, BootstrapFormType formType, bool renderButton = true)
        {
            return builder.Build(helper.ViewData.Model, formType, renderButton);
        }

        #endregion DynamicForm

       
        #endregion Forms

      
    }
}