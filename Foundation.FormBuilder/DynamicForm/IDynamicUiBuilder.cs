using System.Web.Mvc;

namespace Foundation.FormBuilder.DynamicForm
{
    public interface IDynamicUiBuilder<in TModel>
    {
        MvcHtmlString Build(TModel model, BootstrapFormType formType, bool renderButtons);
    }
}