using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.FormBuilder.DynamicForm;

namespace Foundation.FormBuilder.ElementGenerators
{
    public class FormElementGenerator : IElementGenerator
    {
        
        public string RenderElement(FormElement formElement)
        {
            var elementBlock = new StringWriter();
            var writer = new NavHtmlTextWritter(elementBlock);
            
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "form-control");
            switch (formElement.ControlSpecs.ElementType)
            {
                case ElementType.Text:
                    this.RenderTextBox(writer, formElement);
                    break;
                case ElementType.Hidden:
                    this.RenderHidden(writer, formElement);
                    break;
                case ElementType.TextArea:
                    this.RenderTextArea(writer, formElement);
                    break;
                case ElementType.Password:
                    this.RenderPassword(writer, formElement);
                    break;
                case ElementType.DateTime:
                    this.RenderDateTime(writer, formElement);
                    break;
                case ElementType.FloatingPointNumber:
                    this.RenderFloatingPointNumber(writer, formElement);
                    break;
                case ElementType.WholeNumber:
                    this.RenderWholeNumber(writer, formElement);
                    break;
                case ElementType.Time:
                    this.RenderDateTime(writer, formElement);
                    break;
                case ElementType.CheckBox:
                    this.RenderBoolean(writer, formElement);
                    break;
                case ElementType.Enum:
                    this.RenderEnum(writer, formElement);
                    break;
                case ElementType.List:
                    this.RenderDropDownList(writer, formElement);
                    break;
                case ElementType.ListBox:
                    break;
                case ElementType.StaticText:
                    this.RenderStaticText(writer, formElement);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return elementBlock.ToString();
        }
        public virtual void RenderBoolean(NavHtmlTextWritter writer, FormElement formElement)
        {
            var value = formElement.FieldValue;
            if (Convert.ToBoolean(value))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Value, "true");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag(); // input

            writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Value, "false");
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag(); // input
        }

        private void RenderDateTime(NavHtmlTextWritter writer, FormElement formElement)
        {
            var value = formElement.FieldValue;
            writer.AddAttribute(HtmlTextWriterAttribute.Value, Convert.ToString(value));
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "datepicker");
            writer.AddAttribute(HtmlTextWriterAttribute.Cols, "14");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, formElement.PropertyInfo.Name);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag(); // </input>
        }

        private void RenderWholeNumber(NavHtmlTextWritter writer, FormElement formElement)
        {
            var value = formElement.FieldValue;
            writer.AddAttribute(HtmlTextWriterAttribute.Value, Convert.ToString(value));
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, formElement.PropertyInfo.Name);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag(); // input
        }

        private void RenderPassword(NavHtmlTextWritter writer, FormElement formElement)
        {
            var value = formElement.FieldValue;
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "password");
            if (formElement.ControlSpecs.MaxLength != 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Maxlength, formElement.ControlSpecs.MaxLength.ToString(CultureInfo.InvariantCulture));
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Name, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, formElement.PropertyInfo.Name);
            writer.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.Input);
            writer.RenderEndTag(); // </input>
        }

        private void RenderTextBox(NavHtmlTextWritter writer, FormElement formElement)
        {
            var value = formElement.FieldValue;
            writer.AddAttribute(HtmlTextWriterAttribute.Value, Convert.ToString(value));
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            
            if (formElement.ControlSpecs.MaxLength != 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Maxlength, formElement.ControlSpecs.MaxLength.ToString(CultureInfo.InvariantCulture));
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Name, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, formElement.PropertyInfo.Name);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag(); // </input>
        }

        private void RenderHidden(NavHtmlTextWritter writer, FormElement formElement)
        {
            var value = formElement.FieldValue;
            writer.AddAttribute(HtmlTextWriterAttribute.Id, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            var hiddenValue = (value == null) ? String.Empty : value.ToString();
            writer.AddAttribute(HtmlTextWriterAttribute.Value, hiddenValue);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        private void RenderTextArea(NavHtmlTextWritter writer, FormElement formElement)
        {
            var value = formElement.FieldValue;
            
            writer.AddAttribute(HtmlTextWriterAttribute.Name, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Cols, formElement.ControlSpecs.Cols.ToString(CultureInfo.InvariantCulture));
            writer.AddAttribute(HtmlTextWriterAttribute.Rows, formElement.ControlSpecs.Rows.ToString(CultureInfo.InvariantCulture));
            writer.RenderBeginTag(HtmlTextWriterTag.Textarea);
            writer.Write(value);
            writer.RenderEndTag(); // </textarea>
        }

        private void RenderFloatingPointNumber(NavHtmlTextWritter writer, FormElement formElement)
        {
            var value = formElement.FieldValue;
            writer.AddAttribute(HtmlTextWriterAttribute.Value, Convert.ToString(value));
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, formElement.PropertyInfo.Name);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag(); // input
        }

        private void RenderEnum(NavHtmlTextWritter writer, FormElement formElement)
        {
            var value = formElement.FieldValue;
            
            var property = formElement.PropertyInfo;
            var dropDownList = new DropDownList();
            dropDownList.ID = property.Name;

            foreach (var enumValue in Enum.GetValues(property.PropertyType))
            {

                var item = new ListItem(enumValue.ToString().SpacePascal(), enumValue.ToString());

                item.Selected = value != null &&
                                (int) enumValue == (int) Enum.Parse(property.PropertyType, value.ToString());

                dropDownList.Items.Add(item);
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Name, property.Name);
            dropDownList.RenderControl(writer);
        }

        private void RenderDropDownList(NavHtmlTextWritter writer, FormElement formElement)
        {
            var value = formElement.FieldValue;
            
            var itemsList = formElement.CollectionInfo.CollectionObject;

            writer.AddAttribute(HtmlTextWriterAttribute.Name, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, formElement.PropertyInfo.Name);
            writer.RenderBeginTag(HtmlTextWriterTag.Select);
            
            if (itemsList != null)
            {
                foreach (var selectListItem in itemsList)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Value, selectListItem.Value);

                    if (value != null && selectListItem.Value.ToUpper() == value.ToString().ToUpper())
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Selected, null);
                    }

                    writer.RenderBeginTag(HtmlTextWriterTag.Option);
                    writer.Write(selectListItem.Text);
                    writer.RenderEndTag();
                }
            }

            writer.RenderEndTag();
        }

        private void RenderStaticText(NavHtmlTextWritter writer, FormElement formElement)
        {
            var value = formElement.FieldValue;
            var property = formElement.PropertyInfo;
            writer.AddAttribute(HtmlTextWriterAttribute.Name, property.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, property.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "form-control-static");
            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.Write(Convert.ToString(value));
            writer.RenderEndTag(); // input
            RenderHidden(writer, formElement);
        }
    }
}