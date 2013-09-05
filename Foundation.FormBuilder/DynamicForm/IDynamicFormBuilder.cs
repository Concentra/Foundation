using System.Web.Mvc;

namespace Foundation.FormBuilder.DynamicForm
{
    public interface IDynamicFormBuilder<TModel>
    {
        MvcHtmlString Build(TModel model, BootstrapFormType formType);
    }
}