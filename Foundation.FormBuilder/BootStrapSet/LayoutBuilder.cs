using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using Foundation.FormBuilder.DynamicForm;
using Foundation.FormBuilder.Extensions;
using Foundation.Web.Extensions;

namespace Foundation.FormBuilder.BootStrapSet
{
    class LayoutBuilder : ILayoutBuilder
    {
        public string BeginFieldCollection(string groupName, bool useLegend, IFormBuilderParameters formBuilderParameters)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var textWriter = new HtmlTextWriter(stringWriter);

            FieldCollection.Begin(textWriter, groupName, useLegend);
            
            return sb.ToString();
        }

        public string EndFieldCollection(IFormBuilderParameters formBuilderParameters)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var textWriter = new HtmlTextWriter(stringWriter);

            FieldCollection.End(textWriter);

            return sb.ToString();
        }

        public string RenderLabel(FormElement formElement, IFormBuilderParameters formBuilderParameters)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var textWriter = new HtmlTextWriter(stringWriter);

            var bootStrapBuilderParameters = (BuilderParameters)formBuilderParameters;
            // if there is a Name specified in the DisplayAttribute use it , other wise use the property name 
            var displayName = formElement.ControlSpecs.ControlName.SpacePascal();

            if (!String.IsNullOrEmpty(formElement.ControlSpecs.Name))
            {
                displayName = formElement.ControlSpecs.Name;
            }

            // Label
            if (bootStrapBuilderParameters.FormType == BootstrapFormType.Horizontal)
            {
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "col-lg-3 control-label");
            }
            else
            {
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "control-label");
            }

            textWriter.AddAttribute(HtmlTextWriterAttribute.For, formElement.ControlSpecs.ControlName);
            textWriter.RenderBeginTag(HtmlTextWriterTag.Label);
            textWriter.Write(displayName);
            textWriter.RenderEndTag(); // label
            return sb.ToString();
        }

        public string BeginElementContainer(FormElement formElement, IFormBuilderParameters formBuilderParameters)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var textWriter = new HtmlTextWriter(stringWriter);

            ElementContainer.Begin(textWriter, formElement);

            return sb.ToString();
        }

        public string EndElementContainer(FormElement formElement, IFormBuilderParameters formBuilderParameters)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var textWriter = new HtmlTextWriter(stringWriter);

            ElementContainer.End(textWriter);
            return sb.ToString();
        }

        public string BeginControlContainer(FormElement formElement, IFormBuilderParameters formBuilderParameters)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var textWriter = new HtmlTextWriter(stringWriter);
 
            var bootStrapBuilderParameters = (BuilderParameters) formBuilderParameters;
            var formType = bootStrapBuilderParameters.FormType;
            ControlContainer.Begin(textWriter, formType);

            return sb.ToString();
        }

        public string EndControlContainer(FormElement formElement, IFormBuilderParameters formBuilderParameters)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var textWriter = new HtmlTextWriter(stringWriter);

            ControlContainer.End(textWriter);

            return sb.ToString();
        }
    }
}