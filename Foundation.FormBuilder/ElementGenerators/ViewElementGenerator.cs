using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.FormBuilder.DynamicForm;
using Foundation.Web;

namespace Foundation.FormBuilder.ElementGenerators
{
    public class ViewElementGenerator : IElementGenerator
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
            var property = formElement.PropertyInfo;
            var tagbuilder = new TagBuilder("p");

            tagbuilder.MergeAttribute(HtmlAtrributes.Name, property.Name);
            tagbuilder.MergeAttribute(HtmlAtrributes.Id, property.Name);
            tagbuilder.MergeAttribute(HtmlAtrributes.Class, "form-control-static");
            tagbuilder.InnerHtml = Convert.ToString(value);

            return tagbuilder;

        }
    }
}