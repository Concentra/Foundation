namespace Foundation.FormBuilder.CustomAttribute
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class CollectionInfo : System.Attribute
    {
        public string ListSourceMember;
        public string SelectPromptLabel;
        public string SelectPromptValue;
    }
}
