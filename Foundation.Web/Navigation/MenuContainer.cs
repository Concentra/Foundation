using System;
using System.Web.UI;

namespace Foundation.Web.Navigation
{
    internal class MenuContainer : IDisposable
    {
        private readonly HtmlTextWriter htmlTextWriter;

        public MenuContainer(HtmlTextWriter htmlTextWriter)
        {
            this.htmlTextWriter = htmlTextWriter;

            //<nav class="navbar navbar-default" role="navigation">
            this.htmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "nav navbar-inverse");
            this.htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Ul);
        }

        public void Dispose()
        {
            this.htmlTextWriter.RenderEndTag();
        }
    }
}