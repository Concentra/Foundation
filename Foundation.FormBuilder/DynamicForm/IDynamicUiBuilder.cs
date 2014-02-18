using System.Web.Mvc;

namespace Foundation.FormBuilder.DynamicForm
{
    public interface IDynamicUiBuilder<TModel>
    {
        MvcHtmlString Build(TModel model, BootstrapFormType formType, bool renderButtons, HtmlHelper<TModel> htmlHelper);
    }
}