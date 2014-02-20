using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Foundation.FormBuilder.DynamicForm;
using Foundation.Web.Extensions;

namespace Foundation.FormBuilder.BootStrapSet
{
    internal class BootStrapUIBuilder
    {
        private readonly IElementGenerator elementGenerator;
        private readonly ILayoutBuilder layoutBuilder;
        private readonly DynamicForm.FormBuilder formBuilder;

        public BootStrapUIBuilder(Mode viewMode)
        {
            if (viewMode == Mode.View)
            {
                this.elementGenerator = new ViewElementGenerator();
            }
            else
            {
                this.elementGenerator = new FormElementGenerator();
            }
            
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

        public MvcHtmlString BuildElement(BootstrapFormType formType, FormElement formElement)
        {
            var sb = new StringBuilder();
            var formBuilderParameters = new BuilderParameters(formType);

            sb.Append(this.formBuilder.RenderElement(formElement, formBuilderParameters));

            return new MvcHtmlString(sb.ToString());
        }
    }

    internal enum Mode
    {
        Edit,
        View
    }
}