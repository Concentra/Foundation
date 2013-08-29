using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using JQueryUIHelpers;
using VortexSoft.Bootstrap.Collections.Generic;
using VortexSoft.Bootstrap.CustomAttribute;
using VortexSoft.Bootstrap.Extensions;

namespace VortexSoft.Bootstrap
{
    public class BootstrapDynamicFormBuilder<TModel> : IDynamicFormBuilder<TModel>
    {
        protected readonly HtmlHelper<TModel> helper;
        private readonly FormControlGenerator<TModel> formControlGenerator;

        public BootstrapDynamicFormBuilder(HtmlHelper<TModel> helper)
        {
            this.helper = helper;
            formControlGenerator = new FormControlGenerator<TModel>(helper);
        }

        public virtual MvcHtmlString Build(TModel model, BootstrapFormType formType)
        {
            var sb = new StringBuilder(2000);

            using (var stringWriter = new StringWriter(sb))
            using (var textWriter = new NavHtmlTextWritter(stringWriter))
            {
                var formElements = ExtractElementsToRender(model);

                // Find the properties marked with "KeyAttribute" and convert them to hidden fields.

                #region Hidden Fields

                var hiddenFields = new List<FormElement>();

                foreach (var formElement in formElements)
                {
                    var hiddenAttribute = formElement.ControlSpecs;

                    if (hiddenAttribute != null && hiddenAttribute.Control == ControlType.Hidden)
                    {
                        RenderDynamicControl(textWriter, model, formElement);
                    }
                }

                #endregion Hidden Fields

                // if at least one group name is there , then use legend (field container)
                bool useLegend = (formElements.Select(x => x.ControlSpecs.GroupName).Distinct().Count() > 1);

                foreach (
                    var groupedElements in
                        formElements.OrderBy(x => x.ControlSpecs.GroupName).GroupBy(x => x.ControlSpecs.GroupName))
                {
                    textWriter.RenderBeginTag(HtmlTextWriterTag.Fieldset);

                    if (useLegend)
                    {
                        if (!string.IsNullOrEmpty(groupedElements.Key))
                        {
                            textWriter.RenderBeginTag(HtmlTextWriterTag.Legend);
                            textWriter.Write(groupedElements.Key);
                            textWriter.RenderEndTag(); // legend
                        }
                        else
                        {
                            textWriter.RenderBeginTag(HtmlTextWriterTag.Legend);
                            textWriter.Write("General");
                            textWriter.RenderEndTag(); // legend
                        }
                    }

                    // loop over the attributes (ordered)..
                    foreach (var formElement in groupedElements.OrderBy(x => x.ControlSpecs.Order))
                    {
                        // skip those hidden .. 
                        if (formElement.ControlSpecs.Control == ControlType.Hidden)
                        {
                            continue;
                        }

                        // if there is a Name specified in the DisplayAttribute use it , other wise use the property name 
                        var displayName = formElement.PropertyInfo.Name.SpacePascal();

                        if (!string.IsNullOrEmpty(formElement.ControlSpecs.Label))
                        {
                            displayName = formElement.ControlSpecs.Label;
                        }

                        // Control-Group
                        textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "form-group");
                        textWriter.RenderBeginTag(HtmlTextWriterTag.Div);

                        // Label
                        if (formType == BootstrapFormType.Horizontal)
                        {
                            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "col-lg-2 control-label");
                        }
                        else
                        {
                            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "control-label");
                        }
                        textWriter.AddAttribute(HtmlTextWriterAttribute.For, formElement.PropertyInfo.Name);
                        textWriter.RenderBeginTag(HtmlTextWriterTag.Label);
                        textWriter.Write(displayName);
                        textWriter.RenderEndTag(); // label

                        // Controls Div
                        textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "controls col-lg-10");
                        textWriter.RenderBeginTag(HtmlTextWriterTag.Div);

                        // Control
                        RenderDynamicControl(textWriter, model, formElement);
                        
                        textWriter.RenderEndTag(); // div (Controls Div)
                        textWriter.RenderEndTag(); // div (Control-Group)
                    }

                    textWriter.RenderEndTag(); // fieldset
                }

                // Buttons Control-Group
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "form-group");
                textWriter.RenderBeginTag(HtmlTextWriterTag.Div);

                // Controls Div
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "controls");
                textWriter.RenderBeginTag(HtmlTextWriterTag.Div);

                // Submit Button
                textWriter.AddAttribute(HtmlTextWriterAttribute.Type, "submit");
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "btn btn-default btn-primary");
                textWriter.AddAttribute(HtmlTextWriterAttribute.Value, "Submit");
                textWriter.RenderBeginTag(HtmlTextWriterTag.Input);
                textWriter.RenderEndTag(); //</input>

                textWriter.Write("&nbsp;&nbsp;");

                // Cancel Button
                textWriter.AddAttribute(HtmlTextWriterAttribute.Type, "button");
                textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "btn btn-default");
                textWriter.AddAttribute(HtmlTextWriterAttribute.Value, "Cancel");
                textWriter.AddAttribute(HtmlTextWriterAttribute.Onclick,
                                        "window.location = '" + helper.ViewContext.HttpContext.Request.UrlReferrer + "'");
                textWriter.RenderBeginTag(HtmlTextWriterTag.Input);
                textWriter.RenderEndTag(); //</input>

                textWriter.RenderEndTag(); // div (Controls Div)

                textWriter.RenderEndTag(); // div (Buttons Control-Group)

                return new MvcHtmlString(sb.ToString());
            }
        }

        protected virtual List<FormElement> ExtractElementsToRender(TModel model)
        {
            var formElements = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                    .Select(p =>
                                            new FormElement
                                                {
                                                    PropertyInfo = p,
                                                    ControlSpecs =
                                                        p.GetCustomAttributes(typeof (DynamicControl), false)
                                                         .Cast<DynamicControl>()
                                                         .FirstOrDefault()
                                                })
                                    .Where(p => p.ControlSpecs != null)
                                    .ToList();
            return formElements;
        }

        protected void RenderDynamicControl(NavHtmlTextWritter writer, TModel model, FormElement formElement)
        {
            PropertyInfo property = formElement.PropertyInfo;

            bool isRequired = false;

            var requiredAttribute =
                property.GetCustomAttributes(typeof (RequiredAttribute), false).FirstOrDefault() as RequiredAttribute;
            if (requiredAttribute != null)
            {
                isRequired = true;
            }

            var value = property.GetValue(model, null);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "form-control");
            
            switch (formElement.ControlSpecs.Control)
            {
                case ControlType.TextBox:
                    formControlGenerator.RenderTextBox(writer, formElement, value, isRequired);
                    break;
                case ControlType.Hidden:
                    formControlGenerator.RenderHidden(writer, formElement, value, isRequired);
                    break;
                case ControlType.TextArea:
                    formControlGenerator.RenderTextArea(writer, formElement, value, isRequired);
                    break;
                case ControlType.Password:
                    formControlGenerator.RenderPassword(writer, formElement, value, isRequired);
                    break;
                case ControlType.DateTime:
                    formControlGenerator.RenderDateTime(writer, property, value, isRequired);
                    break;
                case ControlType.FloatingPointNumber:
                    formControlGenerator.RenderFloatingPointNumber(writer, property, value, isRequired);
                    break;
                case ControlType.WholeNumber:
                    formControlGenerator.RenderWholeNumber(writer, property, value, isRequired);
                    break;
                case ControlType.Time:
                    formControlGenerator.RenderDateTime(writer, property, value, isRequired);
                    break;
                case ControlType.CheckBox:
                    formControlGenerator.RenderBoolean(writer, property, value);
                    break;
                case ControlType.Enum:
                    formControlGenerator.RenderEnum(writer, property, value, isRequired);
                    break;
                case ControlType.DropDownList:
                    break;
                case ControlType.ListBox:
                    break;
                case ControlType.StaticText:
                    formControlGenerator.RenderStaticText(writer, property, value, isRequired);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}