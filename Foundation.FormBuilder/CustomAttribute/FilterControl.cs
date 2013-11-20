using System;
using System.Globalization;

namespace Foundation.FormBuilder.CustomAttribute
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class FilterControl : System.Attribute
    {
        public Type DataType;
        public bool CaseSensitive;
        public Operator OperatorOption;
        public string DataElement;
    }

    public enum Operator
    {
        Equal,
        Unequal,
        LessThan,
        LessThanOrEqualTo,
        GreaterThan,
        GreaterThanOrEqualTo,
        Like
    }
}
