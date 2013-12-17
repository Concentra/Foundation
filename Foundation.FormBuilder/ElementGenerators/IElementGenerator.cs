using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.FormBuilder.DynamicForm;
using Foundation.Web;

namespace Foundation.FormBuilder.ElementGenerators
{
    public interface IElementGenerator
    {
        TagBuilder RenderElement(FormElement formElement);
    }
}