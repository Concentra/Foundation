using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using Foundation.FormBuilder.DynamicForm;
using Foundation.FormBuilder.Extensions;

namespace Foundation.FormBuilder.Blocks
{
    public class FormContainer : IDisposable
    {
        private readonly HtmlTextWriter textWriter;
        private readonly BootstrapFormType formType;

        public FormContainer(HtmlTextWriter htmlTextWriter,
            string url ,
            BootstrapFormType formType = BootstrapFormType.Horizontal,
            FormMethod method = FormMethod.Post,
            object htmlAttributes = null)
        {
            this.textWriter = htmlTextWriter;
            this.formType = formType;

            textWriter.AddAttribute("method", method.ToString());

            textWriter.AddAttribute("action", url ) ;
            var htmlAttributesCollection = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            

            switch (this.formType)
            {
                case BootstrapFormType.Horizontal: htmlAttributesCollection.Merge("class", "form-horizontal"); break;
                case BootstrapFormType.Vertical: htmlAttributesCollection.Merge("class", "form-vertical"); break;
                case BootstrapFormType.Inline: htmlAttributesCollection.Merge("class", "form-inline"); break;
                case BootstrapFormType.Search: htmlAttributesCollection.Merge("class", "form-search"); break;
            }

            foreach (var htmlAttribute in htmlAttributesCollection)
            {
                textWriter.AddAttribute(htmlAttribute.Key, htmlAttribute.Value.ToString());
            }
            textWriter.RenderBeginTag(HtmlTextWriterTag.Form);
            
        }

        public void Dispose()
        {
            textWriter.RenderEndTag(); // div (ElementType-Group)
        }
    }
}
