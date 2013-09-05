namespace Foundation.FormBuilder.CustomAttribute
{
    [System.AttributeUsage(System.AttributeTargets.Property)
]
    public class DynamicControl : System.Attribute
    {
        public string Label;
        public int Order = int.MaxValue;
        public int Size;
        public int Cols = 60;
        public int Rows = 6;
        public int MaxLength;
        public ControlType Control;
        public bool ReadOnly;
        public string GroupName;
        public string PromptText;
    }

    public enum ControlType
    {
        TextBox,
        Hidden,
        TextArea,
        Password,
        WholeNumber,
        FloatingPointNumber,
        DateTime,
        Time,
        CheckBox,
        Enum,
        DropDownList,
        ListBox,
        StaticText
    }
}
