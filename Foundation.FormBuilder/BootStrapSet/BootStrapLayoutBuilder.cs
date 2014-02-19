using System;
using System.IO;
using System.Text;
using System.Web.UI;
using Foundation.FormBuilder.DynamicForm;
using Foundation.FormBuilder.Extensions;
using Foundation.Web.Extensions;

namespace Foundation.FormBuilder.BootStrapSet
{
    class BootStrapLayoutBuilder : ILayoutBuilder
    {
        private HtmlTextWriter collectionWriter;
        private HtmlTextWriter elementContainerWriter;

        public void BeginFieldCollection(string groupName, bool useLegend, IFormBuilderParameters formBuilderParameters)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            this.collectionWriter = contextWriter;
            collectionWriter.RenderBeginTag(HtmlTextWriterTag.Fieldset); // start field set

            // if at least one group name is there , then use legend (field container)

            if (useLegend)
            {
                collectionWriter.RenderBeginTag(HtmlTextWriterTag.Legend); // start legend tag
                collectionWriter.Write(groupName);
                collectionWriter.RenderEndTag(); // legend
            }
        }

        public void EndFieldCollection(IFormBuilderParameters formBuilderParameters)
        {
            this.collectionWriter.RenderEndTag(); // div (field set)
        }

        public void RenderLabel(HtmlTextWriter textWriter, FormElement formElement, IFormBuilderParameters formBuilderParameters)
        {
            var bootStrapBuilderParameters = (BootStrapBuilderParameters)formBuilderParameters;
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
        }

        public void BeginElementContainer(HtmlTextWriter contextWriter, FormElement formElement, IFormBuilderParameters formBuilderParameters)
        {
            this.elementContainerWriter = contextWriter;
            this.elementContainerWriter.AddAttribute(HtmlTextWriterAttribute.Class, "form-group");

            if (formElement != null && formElement.HasErrors)
            {
                elementContainerWriter.AddAttribute(HtmlTextWriterAttribute.Class, Foundation.Web.Configurations.WebConfigurations.HasErrorsCssClass);
            }

            elementContainerWriter.RenderBeginTag(HtmlTextWriterTag.Div); // div (ElementType-Group)
        }

        public void EndElementContainer(HtmlTextWriter textWriter, IFormBuilderParameters formBuilderParameters)
        {
            this.elementContainerWriter.RenderEndTag(); // div (ElementType-Group)
        }

        public void BeginControlContainer(HtmlTextWriter textWriter, FormElement formElement, IFormBuilderParameters formBuilderParameters)
        {
            var bootStrapBuilderParameters = (BootStrapBuilderParameters) formBuilderParameters;
            var formType = bootStrapBuilderParameters.FormType;
            if (formType == BootstrapFormType.Horizontal)
            {
                // Controls Div
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "col-lg-9");
            }

            textWriter.RenderBeginTag(HtmlTextWriterTag.Div);
            
        }

        public void EndControlContainer(HtmlTextWriter textWriter, FormElement formElement, IFormBuilderParameters formBuilderParameters)
        {
            textWriter.RenderEndTag(); // div (ElementType-Group)
        }
    }
}