using System.Web.Mvc;

namespace Foundation.FormBuilder.DynamicForm
{
    public interface IElementGenerator
    {
        TagBuilder RenderElement(FormElement formElement);
    }
}