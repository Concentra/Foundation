using System;
using System.Web.UI;

namespace Foundation.FormBuilder.BootStrapSet
{
    public class ElementGroup : IDisposable
    {
        private readonly HtmlTextWriter textWriter;

        public ElementGroup(HtmlTextWriter textWriter, string groupName, bool useLegend)
        {
            this.textWriter = textWriter;
            textWriter.RenderBeginTag(HtmlTextWriterTag.Fieldset); // start field set

            // if at least one group name is there , then use legend (field container)

            if (useLegend)
            {
                textWriter.RenderBeginTag(HtmlTextWriterTag.Legend); // start legend tag
                textWriter.Write(groupName);
                textWriter.RenderEndTag(); // legend
            }
        }


        public void Dispose()
        {
            textWriter.RenderEndTag(); // div (field set)
        }
    }
}
