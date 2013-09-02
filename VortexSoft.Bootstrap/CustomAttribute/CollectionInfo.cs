using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VortexSoft.Bootstrap.CustomAttribute
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class CollectionInfo : System.Attribute
    {
        public string ListSourceMember;
        public string SelectPromptLabel;
        public string SelectPromptValue;
    }
}
