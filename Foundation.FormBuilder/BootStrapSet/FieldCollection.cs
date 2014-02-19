using System;
using System.Web.UI;

namespace Foundation.FormBuilder.BootStrapSet
{
    public class FieldCollection 
    {
        
        public static void Begin(HtmlTextWriter textWriter, string groupName, bool useLegend)
        {
            textWriter.RenderBeginTag(HtmlTextWriterTag.Fieldset); // start field set

            // if at least one group name is there , then use legend (field container)
            if (useLegend)
            {
                textWriter.RenderBeginTag(HtmlTextWriterTag.Legend); // start legend tag
                textWriter.Write(groupName);
                textWriter.RenderEndTag(); // legend
            }
        }

        public static void End(HtmlTextWriter textWriter)
        {
            textWriter.WriteEndTag("fieldset"); // div (field set)
        }
    }
}
