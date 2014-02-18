using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

namespace Foundation.FormBuilder.Form
{
    public abstract class BuilderBase<TModel, T> : IDisposable where T : HtmlElement
    {
        // Fields
        protected readonly T element;

        protected readonly TextWriter textWriter;
        protected readonly HtmlHelper<TModel> htmlHelper;

        // Methods
        internal BuilderBase(HtmlHelper<TModel> htmlHelper, T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            this.element = element;
            this.textWriter = htmlHelper.ViewContext.Writer;
            this.textWriter.Write(this.element.StartTag);
            this.htmlHelper = htmlHelper;
        }

        public virtual void Dispose()
        {
            this.textWriter.Write(this.element.EndTag);
        }
    }
}
