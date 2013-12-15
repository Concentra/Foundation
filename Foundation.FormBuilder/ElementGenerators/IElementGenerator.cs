using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.FormBuilder.DynamicForm;

namespace Foundation.FormBuilder.ElementGenerators
{
    public interface IElementGenerator
    {
        TagBuilder RenderElement(FormElement formElement);
    }
}