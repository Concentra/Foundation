using System.Web.Mvc;
using Foundation.FormBuilder.DynamicForm;

namespace Foundation.FormBuilder.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString DynamicForm<TModel>
            (this HtmlHelper<TModel> htmlHelper, BootstrapFormType formType = BootstrapFormType.Horizontal, bool renderButtons = true)
        {
            TModel model = htmlHelper.ViewData.Model;
            return new BootStrapFormBuilder<TModel>()
                .Build(model, formType, renderButtons, htmlHelper);
        }

        public static MvcHtmlString DynamicView<TModel>
             (this HtmlHelper<TModel> htmlHelper, BootstrapFormType formType = BootstrapFormType.Horizontal, bool renderButtons = true)
        {
            TModel model = htmlHelper.ViewData.Model;
            return new BootStrapViewBuilder<TModel>()
                .Build(model, formType, renderButtons, htmlHelper);
            
        }
    }
}