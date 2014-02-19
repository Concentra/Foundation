using System.Web.Mvc;
using Foundation.FormBuilder.DynamicForm;
using Foundation.FormBuilder.Form;

namespace Foundation.FormBuilder.BootStrapSet
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString DynamicForm<TModel>
            (this HtmlHelper<TModel> htmlHelper, BootstrapFormType formType = BootstrapFormType.Horizontal, bool renderButtons = true)
        {
            TModel model = htmlHelper.ViewData.Model;
            var elements = new FormElementsProvider<TModel>().ExtractElementsFromModel(model, htmlHelper);

            return new BootStrapFormBuilder().BuildForm(formType, elements);
                
        }

        public static MvcHtmlString DynamicView<TModel>
             (this HtmlHelper<TModel> htmlHelper, BootstrapFormType formType = BootstrapFormType.Horizontal, bool renderButtons = false)
        {
            TModel model = htmlHelper.ViewData.Model;
            var elements = new FormElementsProvider<TModel>().ExtractElementsFromModel(model, htmlHelper);

            return new BootStrapViewBuilder().BuildForm(formType, elements);
        }

        public static Bootstrap<TModel> Bootstrap<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            return new Bootstrap<TModel>(htmlHelper);
        }
    }
}