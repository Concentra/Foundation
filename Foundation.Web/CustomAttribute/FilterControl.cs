using System;

namespace Foundation.Web.CustomAttribute
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class FilterControl : System.Attribute
    {
        /// <summary>
        /// Whether applying the filter should be case sensitive.
        /// </summary>
        public bool CaseSensitive;
        
        /// <summary>
        /// The operator to apply when filtering
        /// </summary>
        public Operator OperatorOption;

        /// <summary>
        /// The associated data element.
        /// </summary>
        public string DataElement;
    }
}
