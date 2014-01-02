using System;
using System.Web.UI;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.FormBuilder.DynamicForm;
using Foundation.FormBuilder.Extensions;
using Foundation.Web;
using Foundation.Web.Extensions;

namespace Foundation.FormBuilder.Blocks
{
    public class ControlGroup : IDisposable
    {
        private readonly NavHtmlTextWritter textWriter;

        public ControlGroup(NavHtmlTextWritter htmlTextWriter, FormElement formElement = null)
        {
            this.textWriter = htmlTextWriter;
            
            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "form-group");

            if (formElement != null && formElement.HasErrors)
            {
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, Foundation.Web.Configurations.WebConfigurations.HasErrorsCssClass);
            }

            textWriter.RenderBeginTag(HtmlTextWriterTag.Div); // div (ElementType-Group)
        }


        public void Dispose()
        {
            textWriter.RenderEndTag(); // div (ElementType-Group)
        }

        public static void RenderLabel(BootstrapFormType formType, FormElement formElement, NavHtmlTextWritter textWriter)
        {
            // if there is a Name specified in the DisplayAttribute use it , other wise use the property name 
            var displayName = formElement.PropertyInfo.Name.SpacePascal();

            if (!String.IsNullOrEmpty(formElement.ControlSpecs.Name))
            {
                displayName = formElement.ControlSpecs.Name;
            }

            // Label
            if (formType == BootstrapFormType.Horizontal)
            {
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "col-lg-3 control-label");
            }
            else
            {
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "control-label");
            }
            textWriter.AddAttribute(HtmlTextWriterAttribute.For, formElement.PropertyInfo.Name);
            textWriter.RenderBeginTag(HtmlTextWriterTag.Label);
            textWriter.Write(displayName);
            textWriter.RenderEndTag(); // label
        }
    }
}
