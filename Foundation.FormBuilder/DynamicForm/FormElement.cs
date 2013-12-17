using System.Reflection;
using Foundation.FormBuilder.CustomAttribute;

namespace Foundation.FormBuilder.DynamicForm
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