using System;
using System.Globalization;
using System.Web.Mvc;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.FormBuilder.DynamicForm;
using Foundation.FormBuilder.Extensions;

namespace Foundation.FormBuilder.BootStrapSet
{
    internal class FormElementGenerator : IElementGenerator
    {
        
        public TagBuilder RenderElement(FormElement formElement)
        {
            TagBuilder elementTagBuilder = null;

            switch (formElement.ControlSpecs.ElementType)
            {
                case ElementType.Text:
                    elementTagBuilder = this.RenderTextBox(formElement);
                    break;
                case ElementType.Hidden:
                    elementTagBuilder = this.RenderHidden(formElement);
                    break;
                case ElementType.TextArea:
                    elementTagBuilder = this.RenderTextArea(formElement);
                    break;
                case ElementType.Password:
                    elementTagBuilder = this.RenderPassword(formElement);
                    break;
                case ElementType.DateTime:
                    elementTagBuilder = this.RenderDateTime(formElement);
                    break;
                case ElementType.FloatingPointNumber:
                    elementTagBuilder = this.RenderFloatingPointNumber(formElement);
                    break;
                case ElementType.WholeNumber:
                    elementTagBuilder = this.RenderWholeNumber(formElement);
                    break;
                case ElementType.Time:
                    elementTagBuilder = this.RenderDateTime(formElement);
                    break;
                case ElementType.CheckBox:
                    elementTagBuilder = this.RenderBoolean(formElement);
                    break;
                case ElementType.Enum:
                    elementTagBuilder = this.RenderEnum(formElement);
                    break;
                case ElementType.List:
                    elementTagBuilder = this.RenderDropDownList(formElement);
                    break;
                case ElementType.ListBox:
                    break;
                case ElementType.StaticText:
                    elementTagBuilder = this.RenderStaticText(formElement);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (elementTagBuilder != null)
            {
                elementTagBuilder.AddCssClass("form-control");
            }

            return elementTagBuilder;
        }

        public virtual TagBuilder RenderBoolean(FormElement formElement)
        {
            var tagbuilder = new TagBuilder("input");
            var value = formElement.FieldValue;
            
            if (Convert.ToBoolean(value))
            {
                tagbuilder.MergeAttribute(HtmlAtrributes.Checked, "checked");
            }

            tagbuilder.MergeAttribute(HtmlAtrributes.Type, "checkbox");
            tagbuilder.MergeAttribute(HtmlAtrributes.Name, formElement.ControlSpecs.ControlName);
            tagbuilder.MergeAttribute(HtmlAtrributes.Id, formElement.ControlSpecs.ClientId);
            tagbuilder.MergeAttribute(HtmlAtrributes.Value, "true");

            return tagbuilder;
        }

        private TagBuilder RenderDateTime(FormElement formElement)
        {
            var value = formElement.FieldValue;
            var tagbuilder = new TagBuilder("input");
            tagbuilder.MergeAttribute(HtmlAtrributes.Value, Convert.ToString(value));
            tagbuilder.MergeAttribute(HtmlAtrributes.Type, "text");
            tagbuilder.MergeAttribute(HtmlAtrributes.Class, "datepicker");
            tagbuilder.MergeAttribute(HtmlAtrributes.Cols, "14");
            tagbuilder.MergeAttribute(HtmlAtrributes.Id, formElement.ControlSpecs.ClientId);
            tagbuilder.MergeAttribute(HtmlAtrributes.Name, formElement.ControlSpecs.ControlName);

            return tagbuilder;
        }

        private TagBuilder RenderWholeNumber(FormElement formElement)
        {
            var value = formElement.FieldValue;
            var tagbuilder = new TagBuilder("input");
            
            tagbuilder.MergeAttribute(HtmlAtrributes.Value, Convert.ToString(value));
            tagbuilder.MergeAttribute(HtmlAtrributes.Type, "text");
            tagbuilder.MergeAttribute(HtmlAtrributes.Name, formElement.ControlSpecs.ControlName);
            tagbuilder.MergeAttribute(HtmlAtrributes.Id, formElement.ControlSpecs.ClientId);
            return tagbuilder;
        }

        private TagBuilder RenderPassword(FormElement formElement)
        {
            var value = formElement.FieldValue;
            var tagbuilder = new TagBuilder("input");
          
            tagbuilder.MergeAttribute(HtmlAtrributes.Type, "password");
          
            if (formElement.ControlSpecs.MaxLength != 0)
            {
                tagbuilder.MergeAttribute(HtmlAtrributes.Maxlength, formElement.ControlSpecs.MaxLength.ToString(CultureInfo.InvariantCulture));
            }
            tagbuilder.MergeAttribute(HtmlAtrributes.Name, formElement.ControlSpecs.ControlName);
            tagbuilder.MergeAttribute(HtmlAtrributes.Id, formElement.ControlSpecs.ClientId);
            return tagbuilder;
        }

        private TagBuilder RenderTextBox(FormElement formElement)
        {
            var value = formElement.FieldValue;
            var tagbuilder = new TagBuilder("input");
            tagbuilder.MergeAttribute("value", Convert.ToString(value));
            tagbuilder.MergeAttribute(HtmlAtrributes.Type, "text");
            
            if (formElement.ControlSpecs.MaxLength != 0)
            {
                tagbuilder.MergeAttribute(HtmlAtrributes.Maxlength, formElement.ControlSpecs.MaxLength.ToString(CultureInfo.InvariantCulture));
            }

            tagbuilder.MergeAttribute(HtmlAtrributes.Name, formElement.ControlSpecs.ControlName);
            tagbuilder.MergeAttribute(HtmlAtrributes.Id, formElement.ControlSpecs.ClientId);
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

        private TagBuilder RenderTextArea(FormElement formElement)
        {
            var value = formElement.FieldValue;
            var tagbuilder = new TagBuilder("textarea");
            
            tagbuilder.MergeAttribute(HtmlAtrributes.Name, formElement.ControlSpecs.ControlName);
            tagbuilder.MergeAttribute(HtmlAtrributes.Id, formElement.ControlSpecs.ClientId);
            tagbuilder.MergeAttribute(HtmlAtrributes.Cols, formElement.ControlSpecs.Cols.ToString(CultureInfo.InvariantCulture));
            tagbuilder.MergeAttribute(HtmlAtrributes.Rows, formElement.ControlSpecs.Rows.ToString(CultureInfo.InvariantCulture));
            tagbuilder.SetInnerText(Convert.ToString(value));
            return tagbuilder; 
            
        }

        private TagBuilder RenderFloatingPointNumber(FormElement formElement)
        {
            var value = formElement.FieldValue;
            var tagbuilder = new TagBuilder("input");
            tagbuilder.MergeAttribute(HtmlAtrributes.Value, Convert.ToString(value));
            tagbuilder.MergeAttribute(HtmlAtrributes.Type, "text");
            tagbuilder.MergeAttribute(HtmlAtrributes.Name, formElement.ControlSpecs.ControlName);
            tagbuilder.MergeAttribute(HtmlAtrributes.Id, formElement.ControlSpecs.ClientId);
            return tagbuilder; 
        }

        private TagBuilder RenderEnum(FormElement formElement)
        {
            var value = formElement.FieldValue;
            var tagbuilder = new TagBuilder("select");
            
            
            foreach (var enumValue in Enum.GetValues(formElement.MappedDataType))
            {
                var option = new TagBuilder("option");
                option.MergeAttribute(HtmlAtrributes.Value, enumValue.ToString());
                option.SetInnerText(enumValue.ToString().SpacePascal());

                var selected = value != null && (int) enumValue == (int) Enum.Parse(formElement.MappedDataType, value.ToString());

                if (selected)
                {
                    option.MergeAttribute(HtmlAtrributes.Selected, null);
                }

                tagbuilder.InnerHtml += option.ToMvcHtmlString(TagRenderMode.Normal);
            }

            tagbuilder.MergeAttribute("id", formElement.ControlSpecs.ClientId);
            tagbuilder.MergeAttribute(HtmlAtrributes.Name, formElement.ControlSpecs.ControlName);
            return tagbuilder;
        }

        private TagBuilder RenderDropDownList(FormElement formElement)
        {
            var value = formElement.FieldValue;
            
            var itemsList = formElement.CollectionInfo.CollectionObject;
            var tagbuilder = new TagBuilder("select");
           
            tagbuilder.MergeAttribute(HtmlAtrributes.Name, formElement.ControlSpecs.ControlName);
            tagbuilder.MergeAttribute(HtmlAtrributes.Id, formElement.ControlSpecs.ClientId);
            
            if (!string.IsNullOrEmpty(formElement.CollectionInfo.SelectPromptLabel))
            {
                var option = new TagBuilder("option");

                option.MergeAttribute(HtmlAtrributes.Value, formElement.CollectionInfo.SelectPromptValue);
                option.SetInnerText(formElement.CollectionInfo.SelectPromptLabel);

                if (value == null || 
                                    ( formElement.CollectionInfo.SelectPromptValue != null 
                                    && formElement.CollectionInfo.SelectPromptValue.ToUpper() == value.ToString().ToUpper())
                   )
                {
                    option.MergeAttribute(HtmlAtrributes.Selected, null);
                }

                tagbuilder.InnerHtml += option.ToMvcHtmlString(TagRenderMode.Normal);
            }

            if (itemsList != null)
            {
                foreach (var selectListItem in itemsList)
                {
                    var option = new TagBuilder("option");
                    
                    option.MergeAttribute(HtmlAtrributes.Value, selectListItem.Value);
                    option.SetInnerText(selectListItem.Text);
                    
                    if (value != null && selectListItem.Value.ToUpper() == value.ToString().ToUpper())
                    {
                        option.MergeAttribute(HtmlAtrributes.Selected, null);
                    }
                    
                    tagbuilder.InnerHtml += option.ToMvcHtmlString(TagRenderMode.Normal);
                }
            }

            return tagbuilder;
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