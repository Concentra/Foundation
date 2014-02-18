using System.Web.Mvc;
using Foundation.FormBuilder.DynamicForm;

namespace Foundation.FormBuilder
{
    public interface IElementGenerator
    {
        TagBuilder RenderElement(FormElement formElement);
    }
}