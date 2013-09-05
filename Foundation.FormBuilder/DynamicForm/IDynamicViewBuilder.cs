using System.Web.Mvc;

namespace Foundation.FormBuilder.DynamicForm
{
    public interface IDynamicViewBuilder<TModel>
    {
        MvcHtmlString Build(TModel model, BootstrapFormType formType);
    }
}