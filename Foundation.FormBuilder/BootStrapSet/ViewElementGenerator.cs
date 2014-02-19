using System;
using System.Web.Mvc;
using Foundation.FormBuilder.DynamicForm;

namespace Foundation.FormBuilder.BootStrapSet
{
    internal class ViewElementGenerator : IElementGenerator
    {
        
        public TagBuilder RenderElement(FormElement formElement)
        {
            TagBuilder elementTagBuilder = null;

            elementTagBuilder = this.RenderStaticText(formElement);

            return elementTagBuilder;
        }


        private TagBuilder RenderStaticText(FormElement formElement)
        {
            var value = formElement.FieldValue;
            var tagbuilder = new TagBuilder("p");

            tagbuilder.MergeAttribute(HtmlAtrributes.Name, formElement.ControlSpecs.ControlName);
            tagbuilder.MergeAttribute(HtmlAtrributes.Id, formElement.ControlSpecs.ClientId);
            tagbuilder.MergeAttribute(HtmlAtrributes.Class, "form-control-static");
            tagbuilder.InnerHtml = Convert.ToString(value);

            return tagbuilder;

        }
    }
}