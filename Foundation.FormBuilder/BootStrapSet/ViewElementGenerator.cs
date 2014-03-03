using System;
using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.FormBuilder.DynamicForm;

namespace Foundation.FormBuilder.BootStrapSet
{
    internal class ViewElementGenerator : IElementGenerator
    {
        
        public TagBuilder RenderElement(FormElement formElement)
        {
            TagBuilder elementTagBuilder = null;

            if (formElement.ControlSpecs.ElementType != ElementType.Hidden)
            {
                elementTagBuilder = this.RenderStaticText(formElement);
            }
            else
            {
                elementTagBuilder = this.RenderHidden(formElement);
            }

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

        private TagBuilder RenderHidden(FormElement formElement)
        {
            var value = formElement.FieldValue;
            var tagbuilder = new TagBuilder("input");
            tagbuilder.MergeAttribute(HtmlAtrributes.Id, formElement.ControlSpecs.ClientId);
            tagbuilder.MergeAttribute(HtmlAtrributes.Name, formElement.ControlSpecs.ControlName);
            tagbuilder.MergeAttribute(HtmlAtrributes.Type, "hidden");
            var hiddenValue = (value == null) ? String.Empty : value.ToString();
            tagbuilder.MergeAttribute(HtmlAtrributes.Value, hiddenValue);
            return tagbuilder;
        }

    }
}