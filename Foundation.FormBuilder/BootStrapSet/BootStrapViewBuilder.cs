using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Foundation.FormBuilder.DynamicForm;
using Foundation.Web.Extensions;

namespace Foundation.FormBuilder.BootStrapSet
{
    internal class BootStrapViewBuilder
    {
        private readonly IElementGenerator elementGenerator;
        private readonly ILayoutBuilder layoutBuilder;
        private readonly DynamicForm.FormBuilder formBuilder;

        public BootStrapViewBuilder()
        {
            this.elementGenerator = new ViewElementGenerator();
            this.layoutBuilder = new LayoutBuilder();
            this.formBuilder = new DynamicForm.FormBuilder(elementGenerator, layoutBuilder);
        }

        public MvcHtmlString BuildForm(BootstrapFormType formType, List<FormElement> formElements)
        {
            var sb = new StringBuilder();
            var formBuilderParameters = new BuilderParameters(formType);

            sb.Append(this.formBuilder.BuildForm(formElements, formBuilderParameters));

            return new MvcHtmlString(sb.ToString());
        }
    }
}