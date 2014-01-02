namespace Foundation.FormBuilder.CustomAttribute
{
    [System.AttributeUsage(System.AttributeTargets.Property)
]
    public class EditControl : System.Attribute
    {
        /// <summary>
        /// Size for text box.
        /// </summary>
        public int Size;
        
        // Cols attribute of text area. Defaults to 60.
        public int Cols = 60;
        
        // Rows attribute for text area. Defaults to 6.
        public int Rows = 6;
        
        // maxlength attribute of a text box.
        public int MaxLength;
        
        // Element Type to render.
        public ElementType ElementType;
        
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
