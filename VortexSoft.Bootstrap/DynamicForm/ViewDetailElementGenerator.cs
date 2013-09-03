using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using JQueryUIHelpers;
using Foundation.Web.Extensions;
using VortexSoft.Bootstrap.Extensions;
using Foundation.Web.Extensions;

namespace VortexSoft.Bootstrap
{
    public class ViewDetailElementGenerator<TModel>
    {
        private HtmlHelper<TModel> htmlHelper;

        public ViewDetailElementGenerator(HtmlHelper<TModel> htmlHelper)
        {
            this.htmlHelper = htmlHelper;
        }

        public virtual void RenderBoolean(NavHtmlTextWritter writer, PropertyInfo property, object value)
        {
            var textRepresentation = htmlHelper.YesNo(Convert.ToBoolean(value));

            RenderStaticText(writer, property, textRepresentation);
        }

        public virtual void RenderDateTime(NavHtmlTextWritter writer, PropertyInfo property, object value, bool isRequired)
        {
            DateTime? dateTimeValue =  Convert.ToDateTime(value);
            var textRepresentation = dateTimeValue.RenderDate();
            RenderStaticText(writer, property, textRepresentation);
        }

        public virtual void RenderWholeNumber(NavHtmlTextWritter writer, PropertyInfo property, object value, bool isRequired)
        {
            var textRepresentation = value.RoundItOrText("N/A", 0);

            RenderStaticText(writer, property, textRepresentation);
        }
       
        public virtual void RenderFloatingPointNumber(NavHtmlTextWritter writer, PropertyInfo property, object value, bool isRequired)
        {
            var textRepresentation = value.RoundItOrText("N/A", 2);

            RenderStaticText(writer, property, textRepresentation);
        }

        public virtual void RenderEnum(NavHtmlTextWritter writer, PropertyInfo property, object value, bool isRequired)
        {
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

        public virtual void RenderStaticText(NavHtmlTextWritter writer, PropertyInfo property, object value)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Name, property.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, property.Name);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "form-control-static");
            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.Write(Convert.ToString(value));
            writer.RenderEndTag(); // input
        }
    }
}