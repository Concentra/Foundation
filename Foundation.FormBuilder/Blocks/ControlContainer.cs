using System;
using System.Web.UI;

namespace Foundation.FormBuilder.Blocks
{
    class ControlContainer : IDisposable
    {
        private readonly HtmlTextWriter textWriter;

        public ControlContainer(HtmlTextWriter htmlTextWriter)
        {
            this.textWriter = htmlTextWriter;

            // Controls Div
            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "controls col-lg-10");
            textWriter.RenderBeginTag(HtmlTextWriterTag.Div);
        }


        public void Dispose()
        {
            textWriter.RenderEndTag(); // div (Control-Group)
        }
    }
}
