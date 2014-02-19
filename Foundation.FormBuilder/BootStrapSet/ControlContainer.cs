using System;
using System.Web.UI;
using Foundation.FormBuilder.DynamicForm;

namespace Foundation.FormBuilder.BootStrapSet
{
    public class ControlContainer 
    {
        
        public static void Begin(HtmlTextWriter htmlTextWriter, BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            var textWriter = htmlTextWriter;
            if (formType == BootstrapFormType.Horizontal)
            {
                // Controls Div
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "col-lg-9");
            }
            
            textWriter.RenderBeginTag(HtmlTextWriterTag.Div);
            
        }


        public static void End(HtmlTextWriter htmlTextWriter)
        {
            htmlTextWriter.WriteEndTag("div"); // div (ElementType-Group)
        }
    }
}
