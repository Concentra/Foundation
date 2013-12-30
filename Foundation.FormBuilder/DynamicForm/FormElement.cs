using System.Reflection;
using Foundation.FormBuilder.CustomAttribute;

namespace Foundation.FormBuilder.DynamicForm
{
    public class FormElement
    {
        public PropertyInfo PropertyInfo;
        public EditControl ControlSpecs;
        public CollectionInfo CollectionInfo;
        public object FieldValue;
        public bool HasErrors;
    }
}