using System;
using System.Collections.Generic;
using System.Reflection;
using Foundation.FormBuilder.CustomAttribute;

namespace Foundation.FormBuilder.DynamicForm
{
    public class FormElement
    {
        internal PropertyInfo PropertyInfo;
        public EditControl ControlSpecs;
        public CollectionInfo CollectionInfo;
        public object FieldValue;
        public bool HasErrors;
        public Type MappedDataType;
        public IDictionary<string, object> ValidationAttributes;
    }
}