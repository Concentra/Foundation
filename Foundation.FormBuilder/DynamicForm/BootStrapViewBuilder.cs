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
using Foundation.FormBuilder.BootStrapSet;
using Foundation.FormBuilder.CustomAttribute;
using Foundation.FormBuilder.Extensions;
using Foundation.Web.Extensions;

namespace Foundation.FormBuilder.DynamicForm
{
    internal class BootStrapViewBuilder<TModel> : ModelBasedBuilder<TModel>, IDynamicUiBuilder<TModel>
    {
        public BootStrapViewBuilder()
        {
            ElementGenerator = new BootStrapViewElementGenerator();
        }

        public MvcHtmlString Build(TModel model, BootstrapFormType formType, bool renderButtons, HtmlHelper<TModel> htmlHelper)
        {
            var formElements = ExtractElementsToRender(model, htmlHelper);
            
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            using (var textWriter = new NavHtmlTextWritter(stringWriter))
            {
                base.BuildForm(formType, formElements, textWriter);

                if (renderButtons)
                {
                    sb.Append(RenderButtons());
                }

                return new MvcHtmlString(sb.ToString());
            }
        }


        private string RenderButtons()
        {
             var sb = new StringBuilder(2000);
           
            var elementBlock = new StringWriter();
            var textWriter = new NavHtmlTextWritter(elementBlock);
         
            using (new ControlGroup(textWriter, null))
            {
               
                using (new ControlContainer(textWriter))
                {
                    textWriter.Write(RenderButton("submit", "btn btn-default btn-primary", "Submit"));
                    textWriter.Write("&nbsp;&nbsp;");

                    // Cancel Button
                    textWriter.AddAttribute(HtmlTextWriterAttribute.Type, "button");
                    textWriter.AddAttribute(HtmlTextWriterAttribute.Class, "btn btn-default");
                    textWriter.AddAttribute(HtmlTextWriterAttribute.Value, "Cancel");
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