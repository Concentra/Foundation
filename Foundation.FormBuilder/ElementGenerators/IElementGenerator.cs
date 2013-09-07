using Foundation.FormBuilder.CustomAttribute;
using Foundation.FormBuilder.DynamicForm;

namespace Foundation.FormBuilder.ElementGenerators
{
    public interface IElementGenerator
    {
        string RenderElement(FormElement formElement);
    }
}