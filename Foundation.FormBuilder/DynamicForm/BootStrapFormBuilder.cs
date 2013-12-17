using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Foundation.FormBuilder.Blocks;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.FormBuilder.ElementGenerators;

namespace Foundation.FormBuilder.DynamicForm
{
    public class BootStrapFormBuilder<TModel> : UiBuilderBase<TModel>, IDynamicUiBuilder<TModel>
    {
        public BootStrapFormBuilder()
        {
            ElementGenerator = new FormElementGenerator();
            Properties = typeof(TModel).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(p => p.Name , p => p);
        }

        public MvcHtmlString Build(TModel model, BootstrapFormType formType, bool renderButtons, HtmlHelper<TModel> htmlelper)
        {
            
            var formElements = ExtractElementsToRender(model);
            
            var sb = new StringBuilder();
            sb.Append(RenderHiddenFields(formElements));
            var stringWriter = new StringWriter(sb);
            using (var textWriter = new NavHtmlTextWritter(stringWriter))
            {
                BuildLayout(htmlelper, formType, formElements, textWriter);

                if (renderButtons)
                {
                    sb.Append(RenderButtons());
                }

                return new MvcHtmlString(sb.ToString());
            }
        }


        private string RenderHiddenFields(IEnumerable<FormElement> formElements)
        {
            var hiddenBlock = new StringBuilder();
            foreach (var formElement in formElements)
            {
                var hiddenAttribute = formElement.ControlSpecs;

                if (hiddenAttribute != null && hiddenAttribute.ElementType == ElementType.Hidden)
                {
                    hiddenBlock.Append(ElementGenerator.RenderElement(formElement));
                }
            }

            return hiddenBlock.ToString();
        }

        private string RenderButtons()
        {
             var sb = new StringBuilder(2000);
           
            var elementBlock = new StringWriter();
            var textWriter = new NavHtmlTextWritter(elementBlock);
         
            using (new ControlGroup(textWriter))
            {
               
                using (new ControlContainer(textWriter))
                {
                    textWriter.Write(RenderButton("submit", "btn btn-default btn-primary", "submit"));
                    textWriter.Write("&nbsp;&nbsp;");

                    // Cancel Button
                    textWriter.AddAttribute(HtmlTextWriterAttribute.Type, "button");
                    textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "btn btn-default");
                    textWriter.AddAttribute(HtmlTextWriterAttribute.Value, "Cancel");
                    textWriter.AddAttribute(HtmlTextWriterAttribute.Name, "CancelBtn");
                    textWriter.AddAttribute(HtmlTextWriterAttribute.Onclick,
                        "window.location = '" + HttpContext.Current.Request.UrlReferrer + "'");
                    textWriter.RenderBeginTag((HtmlTextWriterTag)HtmlTextWriterTag.Input);
                    textWriter.RenderEndTag(); //</input>

          
                }
            }

            return sb.Append(elementBlock).ToString();
        }

        private string RenderButton(string buttonType, string CssClass, string buttonValue)
        {
            var elementBlock = new StringWriter();
            var textWriter = new NavHtmlTextWritter(elementBlock);
            textWriter.AddAttribute(HtmlTextWriterAttribute.Type, buttonType);
            textWriter.AddAttribute(HtmlTextWriterAttribute.Class, CssClass);
            textWriter.AddAttribute(HtmlTextWriterAttribute.Value, buttonValue);
            textWriter.RenderBeginTag(HtmlTextWriterTag.Input);
            textWriter.RenderEndTag(); //</input>

            var returnValue = elementBlock.ToString();
            return returnValue;
        }
    }
}