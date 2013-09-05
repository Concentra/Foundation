using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using Foundation.FormBuilder.Blocks;
using Foundation.FormBuilder.CustomAttribute;

namespace Foundation.FormBuilder.DynamicForm
{
    public class BootstrapDynamicFormBuilder<TModel> : IDynamicFormBuilder<TModel>
    {
        protected readonly HtmlHelper<TModel> helper;
        private readonly FormControlGenerator<TModel> formControlGenerator;
        private readonly Dictionary<string, PropertyInfo> properties;

        public BootstrapDynamicFormBuilder(HtmlHelper<TModel> helper)
        {
            this.helper = helper;
            formControlGenerator = new FormControlGenerator<TModel>(helper);
            properties = typeof(TModel).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(p => p.Name , p => p);
        }

        public virtual MvcHtmlString Build(TModel model, BootstrapFormType formType, bool renderButtons = true)
        {
            var sb = new StringBuilder(2000);
            var formElements = ExtractElementsToRender(model);
            var groupsofElements = formElements.OrderBy(x => x.ControlSpecs.GroupName).GroupBy(x => x.ControlSpecs.GroupName);
            bool useLegend = (formElements.Select(x => x.ControlSpecs.GroupName).Distinct().Count() > 1);

            var stringWriter = new StringWriter(sb);

            using (var textWriter = new NavHtmlTextWritter(stringWriter))
            {
                
                RenderHiddenFields(model, formElements, textWriter);

                foreach (var groupedElements in groupsofElements)
                {
                    var groupName = (!String.IsNullOrEmpty(groupedElements.Key)) ? groupedElements.Key : "General";
           
                    using (new ElementGroup(textWriter, groupName, useLegend))
                    {
                        var elementsToRender = groupedElements.Where(x => x.ControlSpecs.Control != ControlType.Hidden)
                            .OrderBy(x => x.ControlSpecs.Order);

                        // loop over the attributes (ordered)..
                        foreach (var formElement in elementsToRender)
                        {
                            
                            using (new ControlGroup(textWriter))
                            {
                                ControlGroup.RenderLabel(formType, formElement, textWriter);

                                using (new ControlContainer(textWriter))
                                {
                                    // Control
                                    RenderElement(textWriter, model, formElement);
                                }
                            }
                        }
                    }
               }

                if (renderButtons)
                {
                    RenderButtons(textWriter);
                }

                return new MvcHtmlString(sb.ToString());
            }
        }

       
        private void RenderHiddenFields(TModel model, IEnumerable<FormElement> formElements, NavHtmlTextWritter textWriter)
        {
            foreach (var formElement in formElements)
            {
                var hiddenAttribute = formElement.ControlSpecs;

                if (hiddenAttribute != null && hiddenAttribute.Control == ControlType.Hidden)
                {
                    RenderElement(textWriter, model, formElement);
                }
            }
        }

        private void RenderButtons(NavHtmlTextWritter textWriter)
        {
            // Buttons Control-Group
            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "form-group");
            textWriter.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.Div);

            // Controls Div
            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "controls");
            textWriter.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.Div);

            // Submit Button
            textWriter.AddAttribute(HtmlTextWriterAttribute.Type, "submit");
            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "btn btn-default btn-primary");
            textWriter.AddAttribute(HtmlTextWriterAttribute.Value, "Submit");
            textWriter.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.Input);
            textWriter.RenderEndTag(); //</input>

            textWriter.Write("&nbsp;&nbsp;");

            // Cancel Button
            textWriter.AddAttribute(HtmlTextWriterAttribute.Type, "button");
            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "btn btn-default");
            textWriter.AddAttribute(HtmlTextWriterAttribute.Value, "Cancel");
            textWriter.AddAttribute(HtmlTextWriterAttribute.Onclick,
                "window.location = '" + helper.ViewContext.HttpContext.Request.UrlReferrer + "'");
            textWriter.RenderBeginTag((HtmlTextWriterTag) HtmlTextWriterTag.Input);
            textWriter.RenderEndTag(); //</input>

            textWriter.RenderEndTag(); // div (Controls Div)

            textWriter.RenderEndTag(); // div (Buttons Control-Group)
        }

        public List<FormElement> ExtractElementsToRender(TModel model)
        {
            var formElements = properties.Select(p =>
                                            new FormElement
                                                {
                                                    PropertyInfo = p.Value,
                                                    ControlSpecs =
                                                        p.Value.GetCustomAttributes(typeof (DynamicControl), false)
                                                         .Cast<DynamicControl>()
                                                         .FirstOrDefault(),
                                                    CollectionInfo = p.Value.GetCustomAttributes(typeof(CollectionInfo), false)
                                                         .Cast<CollectionInfo>()
                                                         .FirstOrDefault(),
                                                })
                                    .Where(p => p.ControlSpecs != null)
                                    .ToList();
            return formElements;
        }

        protected void RenderElement(NavHtmlTextWritter writer, TModel model, FormElement formElement)
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
                    formControlGenerator.RenderDateTime(writer, formElement, value, isRequired);
                    break;
                case ControlType.FloatingPointNumber:
                    formControlGenerator.RenderFloatingPointNumber(writer, formElement, value, isRequired);
                    break;
                case ControlType.WholeNumber:
                    formControlGenerator.RenderWholeNumber(writer, formElement, value, isRequired);
                    break;
                case ControlType.Time:
                    formControlGenerator.RenderDateTime(writer, formElement, value, isRequired);
                    break;
                case ControlType.CheckBox:
                    formControlGenerator.RenderBoolean(writer, formElement, value);
                    break;
                case ControlType.Enum:
                    formControlGenerator.RenderEnum(writer, formElement, value, isRequired);
                    break;
                case ControlType.DropDownList:
                    var collectionObject = properties[formElement.CollectionInfo.ListSourceMember]
                        .GetValue(model, null) as IEnumerable<SelectListItem>;
                    formControlGenerator.RenderDropDownList(writer, formElement, value, isRequired, collectionObject);
                    break;
                case ControlType.ListBox:
                    break;
                case ControlType.StaticText:
                    formControlGenerator.RenderStaticText(writer, formElement, value, isRequired);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}