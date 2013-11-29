namespace Foundation.FormBuilder.CustomAttribute
{
    [System.AttributeUsage(System.AttributeTargets.Property)
]
    public class EditControl : System.Attribute
    {
        public string Label;
        public int Order = int.MaxValue;
        public int Size;
        public int Cols = 60;
        public int Rows = 6;
        public int MaxLength;
        public ElementType ElementType;
        public bool ReadOnly;
        public string GroupName;
        public string PromptText;
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
