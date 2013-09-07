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
    public class ViewElementGenerator : IElementGenerator
    {
        
        public string RenderElement(FormElement formElement)
        {
            var elementBlock = new StringWriter();
            var writer = new NavHtmlTextWritter(elementBlock);
            
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "form-control");
            this.RenderStaticText(writer, formElement);

            #region 
            /*
            switch (formElement.ControlSpecs.Control)
            {
                case ControlType.TextBox:
                    this.RenderTextBox(writer, formElement);
                    break;
                case ControlType.Hidden:
                    this.RenderHidden(writer, formElement);
                    break;
                case ControlType.TextArea:
                    this.RenderTextArea(writer, formElement);
                    break;
                case ControlType.Password:
                    this.RenderPassword(writer, formElement);
                    break;
                case ControlType.DateTime:
                    this.RenderDateTime(writer, formElement);
                    break;
                case ControlType.FloatingPointNumber:
                    this.RenderFloatingPointNumber(writer, formElement);
                    break;
                case ControlType.WholeNumber:
                    this.RenderWholeNumber(writer, formElement);
                    break;
                case ControlType.Time:
                    this.RenderDateTime(writer, formElement);
                    break;
                case ControlType.CheckBox:
                    this.RenderBoolean(writer, formElement);
                    break;
                case ControlType.Enum:
                    this.RenderEnum(writer, formElement);
                    break;
                case ControlType.DropDownList:
                    this.RenderDropDownList(writer, formElement);
                    break;
                case ControlType.ListBox:
                    break;
                case ControlType.StaticText:

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            */
            #endregion

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
            
            DateTime? dateTimeValue;
            if (value != null)
            {
                dateTimeValue = Convert.ToDateTime(value);
            }
            else
            {
                dateTimeValue = null;
            }

            var datePicker = string.Empty;
            //htmlHelper.JQueryUI().Datepicker(formElement.PropertyInfo.Name, dateTimeValue).ChangeYear(true).ChangeMonth(true);
            writer.ClearAttributes();
            writer.Write(datePicker);
        }

        private void RenderWholeNumber(NavHtmlTextWritter writer, FormElement formElement)
        {
            var value = formElement.FieldValue;
            var isRequired = formElement.ValidationInfo.Required;

            if (isRequired)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "validate[required,custom[integer]]");
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "validate[custom[integer]]");
            }
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
            var isRequired = formElement.ValidationInfo.Required;
            
            if (isRequired)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "validate[required]");
            }
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
            var isRequired = formElement.ValidationInfo.Required;

            if (isRequired)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "validate[required]");
            }
            
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
            var isRequired = formElement.ValidationInfo.Required;

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
            var isRequired = formElement.ValidationInfo.Required;

            if (isRequired)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "validate[required]");
            }

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
            var isRequired = formElement.ValidationInfo.Required;

            if (isRequired)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "validate[required,custom[number]]");
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "validate[custom[number]]");
            }
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
            var isRequired = formElement.ValidationInfo.Required;

            var property = formElement.PropertyInfo;
            var dropDownList = new DropDownList();
            dropDownList.ID = property.Name;

            foreach (var fieldInfo in property.PropertyType.GetFields(BindingFlags.Public | BindingFlags.Static).OrderBy(x => x.Name))
            {
                var item = new ListItem(fieldInfo.Name.SpacePascal(), fieldInfo.GetRawConstantValue().ToString());
                dropDownList.Items.Add(item);
            }

            if (isRequired)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "validate[required]");
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Name, property.Name);
            dropDownList.RenderControl(writer);
        }

        private void RenderDropDownList(NavHtmlTextWritter writer, FormElement formElement)
        {
            var value = formElement.FieldValue;
            var isRequired = formElement.ValidationInfo.Required;
            
            var itemsList = formElement.CollectionInfo.CollectionObject;

            if (isRequired)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "validate[required]");
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Name, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, formElement.PropertyInfo.Name);
            writer.RenderBeginTag(HtmlTextWriterTag.Select);
            
            if (itemsList != null)
            {
                foreach (var selectListItem in itemsList)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Value, selectListItem.Value);

                    if (value != null && selectListItem.Value == value.ToString())
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Selected, null);
                    }

                    writer.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.Option);
                    writer.Write(selectListItem.Value);
                    writer.RenderEndTag();
                }
            }

            writer.RenderEndTag();
        }

        private void RenderStaticText(NavHtmlTextWritter writer, FormElement formElement)
        {
            var value = formElement.FieldValue;
            var isRequired = formElement.ValidationInfo != null && formElement.ValidationInfo.Required;
            var property = formElement.PropertyInfo;
            writer.AddAttribute(HtmlTextWriterAttribute.Name, property.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, property.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "form-control-static");
            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.Write(Convert.ToString(value));
            writer.RenderEndTag(); // input
        }
    }
}