using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Foundation.FormBuilder.Form
{
    public class FormBuilder<TModel> : BuilderBase<TModel, BootstrapForm>
    {
        internal BootstrapFormBuilder(HtmlHelper<TModel> htmlHelper, BootstrapForm form)
            : base(htmlHelper, form)
        {
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
