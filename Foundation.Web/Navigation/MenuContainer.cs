using System;
using System.Web.UI;
using Foundation.FormBuilder.Extensions;

namespace Foundation.Web.Navigation
{
    internal class MenuContainer : IDisposable
    {
        private readonly HtmlTextWriter htmlTextWriter;

        public MenuContainer(NavHtmlTextWritter htmlTextWriter)
        {
            this.htmlTextWriter = htmlTextWriter;

            //<nav class="navbar navbar-default" role="navigation">
            this.htmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "nav navbar-nav");
            this.htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Ul);
        }

        public void Dispose()
        {
            this.htmlTextWriter.RenderEndTag();
        }
    }
}