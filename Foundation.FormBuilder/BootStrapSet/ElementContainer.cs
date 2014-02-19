using System;
using System.Web.UI;
using Foundation.FormBuilder.DynamicForm;
using Foundation.FormBuilder.Extensions;
using Foundation.Web.Extensions;

namespace Foundation.FormBuilder.BootStrapSet
{
    public class ElementContainer 
    {
        public static void Begin(HtmlTextWriter textWriter, FormElement formElement)
        {
            
            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "form-group");

            if (formElement != null && formElement.HasErrors)
            {
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, Foundation.Web.Configurations.WebConfigurations.HasErrorsCssClass);
            }

            textWriter.RenderBeginTag(HtmlTextWriterTag.Div); // div (ElementType-Group)
        }


        public static void End(HtmlTextWriter textWriter)
        {
            textWriter.WriteEndTag("div"); // div (ElementType-Group)
        }
    }
}
