namespace Foundation.FormBuilder.CustomAttribute
{
    [System.AttributeUsage(System.AttributeTargets.Property)
]
    public class EditControl : System.Attribute
    {
        public int Size;
        public int Cols = 60;
        public int Rows = 6;
        public int MaxLength;
        public ElementType ElementType;
        public bool ReadOnly;
        internal string GroupName;
        internal string Prompt;
        internal string ShortName;
        internal int? Order = int.MaxValue;
        internal string Name;
    }

    public enum ElementType
    {
        Text,
        Hidden,
        TextArea,
        Password,
        WholeNumber,
        FloatingPointNumber,
        DateTime,
        Time,
        CheckBox,
        Enum,
        List,
        ListBox,
        StaticText,
        Guid
    }
}
