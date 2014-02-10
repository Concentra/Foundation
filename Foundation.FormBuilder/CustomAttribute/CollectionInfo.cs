using System.Collections.Generic;
using System.Web.Mvc;

namespace Foundation.FormBuilder.CustomAttribute
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class CollectionInfo : System.Attribute
    {
        /// <summary>
        /// The name of the property that contains the list of items. Must be of type  IEnumerable &lt;SelectListItem&gt;
        /// </summary>
        public string ListSourceMember;
        
        /// <summary>
        /// The "Please select an items" text .
        /// </summary>
        public string SelectPromptLabel;

        /// <summary>
        /// The "Please select an items" associated value.
        /// </summary>
        public string SelectPromptValue;
        internal IEnumerable<SelectListItem> CollectionObject;
    }
}
