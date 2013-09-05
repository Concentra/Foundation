using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using JQueryUIHelpers;

namespace Foundation.FormBuilder.DynamicForm
{
    public class FormControlGenerator<TModel>
    {
        private readonly HtmlHelper<TModel> htmlHelper;

        public FormControlGenerator(HtmlHelper<TModel> htmlHelper)
        {
            this.htmlHelper = htmlHelper;
        }

        public virtual void RenderBoolean(NavHtmlTextWritter writer, FormElement formElement, object value)
        {
            if (Convert.ToBoolean(value))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Value, "true");
            writer.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.Input);
            writer.RenderEndTag(); // input

            writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            writer.AddAttribute(HtmlTextWriterAttribute.Name, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Value, "false");
            writer.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.Input);
            writer.RenderEndTag(); // input
        }

        public virtual void RenderDateTime(NavHtmlTextWritter writer, FormElement formElement, object value, bool isRequired)
        {
            DateTime? dateTimeValue;
            if (value != null)
            {
                dateTimeValue = Convert.ToDateTime(value);
            }
            else
            {
                dateTimeValue = null;
            }
            
            var datePicker = htmlHelper.JQueryUI().Datepicker(formElement.PropertyInfo.Name, dateTimeValue).ChangeYear(true).ChangeMonth(true);
            writer.ClearAttributes();
            writer.Write(datePicker.ToHtmlString());
        }

        public virtual void RenderWholeNumber(NavHtmlTextWritter writer, FormElement formElement, object value, bool isRequired)
        {
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
            writer.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.Input);
            writer.RenderEndTag(); // input
        }

        public virtual void RenderPassword(NavHtmlTextWritter writer, FormElement formElement, object value, bool isRequired)
        {
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

        public virtual void RenderTextBox(NavHtmlTextWritter writer, FormElement formElement, object value, bool isRequired)
        {
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
            writer.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.Input);
            writer.RenderEndTag(); // </input>
        }

        public virtual void RenderHidden(NavHtmlTextWritter writer, FormElement formElement, object value, bool isRequired)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
            var hiddenValue = (value == null) ? String.Empty : value.ToString();
            writer.AddAttribute(HtmlTextWriterAttribute.Value, hiddenValue);
            writer.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }

        public virtual void RenderTextArea(NavHtmlTextWritter writer, FormElement formElement, object value, bool isRequired)
        {
            if (isRequired)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "validate[required]");
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Name, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Cols, formElement.ControlSpecs.Cols.ToString(CultureInfo.InvariantCulture));
            writer.AddAttribute(HtmlTextWriterAttribute.Rows, formElement.ControlSpecs.Rows.ToString(CultureInfo.InvariantCulture));
            writer.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.Textarea);
            writer.Write(value);
            writer.RenderEndTag(); // </textarea>
        }

        public virtual void RenderFloatingPointNumber(NavHtmlTextWritter writer, FormElement formElement, object value, bool isRequired)
        {
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
            writer.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.Input);
            writer.RenderEndTag(); // input
        }

        public virtual void RenderEnum(NavHtmlTextWritter writer, FormElement formElement, object value, bool isRequired)
        {
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

        public virtual void RenderDropDownList(NavHtmlTextWritter writer, FormElement formElement, object value,
                                               bool isRequired, IEnumerable<SelectListItem> itemsList)
        {
            var property = formElement.PropertyInfo;

            if (isRequired)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "validate[required]");
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Name, formElement.PropertyInfo.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, formElement.PropertyInfo.Name);
            writer.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.Select);
            
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

        public virtual void RenderStaticText(NavHtmlTextWritter writer, FormElement formElement, object value, bool isRequired)
        {
            var property = formElement.PropertyInfo;
            writer.AddAttribute(HtmlTextWriterAttribute.Name, property.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, property.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "form-control-static");
            writer.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.P);
            writer.Write(Convert.ToString(value));
            writer.RenderEndTag(); // input
        }

        public static void RenderGroupLegend(NavHtmlTextWritter textWriter, string groupName, bool useLegend)
        {
            textWriter.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.Fieldset);

            // if at least one group name is there , then use legend (field container)
         
            if (useLegend)
            {
                textWriter.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.Legend); // start legend tag
                textWriter.Write(groupName);
                textWriter.RenderEndTag(); // legend
            }
        }
    }
}