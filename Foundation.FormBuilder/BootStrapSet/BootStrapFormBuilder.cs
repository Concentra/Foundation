using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Foundation.FormBuilder.DynamicForm;
using Foundation.Web.Extensions;

namespace Foundation.FormBuilder.BootStrapSet
{
    internal class BootStrapFormBuilder 
    {
        private readonly IElementGenerator elementGenerator;
        private readonly ILayoutBuilder layoutBuilder;
        private readonly DynamicForm.FormBuilder formBuilder;

        public BootStrapFormBuilder()
        {
            this.elementGenerator = new FormElementGenerator();
            this.layoutBuilder = new LayoutBuilder();
            this.formBuilder = new DynamicForm.FormBuilder(elementGenerator, layoutBuilder);
        }

        public MvcHtmlString BuildForm(BootstrapFormType formType, List<FormElement> formElements)
        {
            var formBuilderParameters = new BuilderParameters(formType);

            var formMarkUp = this.formBuilder.BuildForm(formElements, formBuilderParameters);

            return new MvcHtmlString(formMarkUp);
        }

    }
}