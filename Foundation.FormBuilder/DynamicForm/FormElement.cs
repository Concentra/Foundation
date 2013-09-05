using System.Reflection;
using Foundation.FormBuilder.CustomAttribute;

namespace Foundation.FormBuilder.DynamicForm
{
    public class FormElement
    {
        public PropertyInfo PropertyInfo;
        public DynamicControl ControlSpecs;
        public CollectionInfo CollectionInfo;
    }
}