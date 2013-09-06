using System;
using System.Web.UI;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.FormBuilder.DynamicForm;

namespace Foundation.FormBuilder.Blocks
{
    class ControlGroup : IDisposable
    {
        private readonly HtmlTextWriter textWriter;

        public ControlGroup(HtmlTextWriter htmlTextWriter)
        {
            this.textWriter = htmlTextWriter;
            
            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "form-group");

            textWriter.RenderBeginTag(HtmlTextWriterTag.Div); // div (Control-Group)
        }


        public void Dispose()
        {
            textWriter.RenderEndTag(); // div (Control-Group)
        }

        public static void RenderLabel(BootstrapFormType formType, FormElement formElement, NavHtmlTextWritter textWriter)
        {
            // if there is a Name specified in the DisplayAttribute use it , other wise use the property name 
            var displayName = formElement.PropertyInfo.Name.SpacePascal();

            if (!String.IsNullOrEmpty(formElement.ControlSpecs.Label))
            {
                displayName = formElement.ControlSpecs.Label;
            }

            // Label
            if (formType == BootstrapFormType.Horizontal)
            {
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "col-lg-2 control-label");
            }
            else
            {
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "control-label");
            }
            textWriter.AddAttribute(HtmlTextWriterAttribute.For, formElement.PropertyInfo.Name);
            textWriter.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.Label);
            textWriter.Write(displayName);
            textWriter.RenderEndTag(); // label
        }
    }
}
