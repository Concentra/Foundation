using System;
using System.Web.Mvc;

namespace Foundation.Web.Extensions
{
    public class MvcPanel : IDisposable
    {
        private readonly HtmlHelper htmlHelper;
        private bool disposed;

        public MvcPanel(HtmlHelper htmlHelper)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }

            this.htmlHelper = htmlHelper;
        }

        public static void EndPanel(HtmlHelper htmlHelper)
        {
            var writer = htmlHelper.ViewContext.Writer;

            writer.Write("</div></div>");
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                this.disposed = true;
                EndPanel(this.htmlHelper);
            }
        }
    }
}