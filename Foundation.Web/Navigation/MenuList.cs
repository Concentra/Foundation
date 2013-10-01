using System;
using System.Web.UI;

namespace Foundation.Web.Navigation
{
    internal class MenuList : IDisposable
    {
        private readonly HtmlTextWriter htmlTextWriter;

        public MenuList(HtmlTextWriter htmlTextWriter, bool isRoot = false)
        {
            this.htmlTextWriter = htmlTextWriter;

            //<nav class="navbar navbar-default" role="navigation">
            if (isRoot)
            {
                this.htmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "navbar navbar-nav");
            }
            else
            {
                this.htmlTextWriter.AddAttribute(HtmlTextWriterAttribute.Class, "dropdown-menu");
            }

            this.htmlTextWriter.RenderBeginTag(HtmlTextWriterTag.Ul);
        }

        public void Dispose()
        {
            this.htmlTextWriter.RenderEndTag();
        }
    }
}