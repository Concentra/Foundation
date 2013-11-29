using System;
using System.Web.UI;
using Foundation.FormBuilder.DynamicForm;

namespace Foundation.FormBuilder.Blocks
{
    class ControlContainer : IDisposable
    {
        private readonly HtmlTextWriter textWriter;
        private readonly BootstrapFormType formType;

        public ControlContainer(HtmlTextWriter htmlTextWriter, BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            this.textWriter = htmlTextWriter;
            this.formType = formType;
            if (formType == BootstrapFormType.Horizontal)
            {
                // Controls Div
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "col-lg-9");
            }
            
            textWriter.RenderBeginTag(HtmlTextWriterTag.Div);
            
        }


        public void Dispose()
        {
            textWriter.RenderEndTag(); // div (ElementType-Group)
        }
    }
}
