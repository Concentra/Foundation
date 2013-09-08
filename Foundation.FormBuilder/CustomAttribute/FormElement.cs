using System.Reflection;
using Foundation.FormBuilder.DynamicForm;

namespace Foundation.FormBuilder.CustomAttribute
{
    public class FormElement
    {
        public PropertyInfo PropertyInfo;
        public EditControl ControlSpecs;
        public CollectionInfo CollectionInfo;
        public ValidationInfo ValidationInfo;
        public object FieldValue;
    }
}