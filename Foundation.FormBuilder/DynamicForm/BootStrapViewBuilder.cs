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
    public class BootStrapViewBuilder<TModel> : UiBuilderBase, IDynamicUiBuilder<TModel>
    {
        private readonly Dictionary<string, PropertyInfo> properties;

        public BootStrapViewBuilder()
        {
            ElementGenerator = new ViewElementGenerator();
            properties = typeof(TModel).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(p => p.Name , p => p);
        }

        public MvcHtmlString Build(TModel model, BootstrapFormType formType, bool renderButtons = false)
        {
            var formElements = ExtractElementsToRender(model);
            
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            using (var textWriter = new NavHtmlTextWritter(stringWriter))
            {
                BuildLayout(formType, formElements, textWriter);

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
         
            using (new ControlGroup(textWriter))
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

        private List<FormElement> ExtractElementsToRender(TModel model)
        {
            var formElements = properties.Select(p =>
                                            new FormElement
                                                {
                                                    PropertyInfo = p.Value,
                                                    ControlSpecs =
                                                        p.Value.GetCustomAttributes(typeof (EditControl), false)
                                                         .Cast<EditControl>()
                                                         .FirstOrDefault(),
                                                    CollectionInfo = p.Value.GetCustomAttributes(typeof(CollectionInfo), false)
                                                         .Cast<CollectionInfo>()
                                                         .FirstOrDefault(),
                                                    FieldValue = p.Value.GetValue(model, null),
                                                    ValidationInfo = null
                                                })
                                    .Where(p => p.ControlSpecs != null)
                                    .ToList();

            foreach (var collectionItems in formElements.Where(x => x.CollectionInfo != null))
            {
                collectionItems.CollectionInfo.CollectionObject =
                    properties[collectionItems.CollectionInfo.ListSourceMember]
                        .GetValue(model, null) as IEnumerable<SelectListItem>;
            }

            return formElements;
        }
    }
}