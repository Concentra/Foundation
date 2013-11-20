using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation.FormBuilder.CustomAttribute;

namespace Kafala.Test
{
    class FilterModel
    {
        [FilterControl(DataElement = "Module.Name")]
        public string PropertyName { get; set; }

        [FilterControl(DataElement = "ReflectedType.FullName")]
        public string ReflectedType { get; set; }

        [FilterControl(DataElement = "ReflectedType.GUID")]
        public Guid TypeGuid { get; set; }
    }
}
