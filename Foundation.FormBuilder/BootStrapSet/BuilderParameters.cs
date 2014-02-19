using Foundation.FormBuilder.DynamicForm;

namespace Foundation.FormBuilder.BootStrapSet
{
    public class BuilderParameters : IFormBuilderParameters

    {
        private readonly BootstrapFormType formType;

        public BuilderParameters(BootstrapFormType formType)
        {
            this.formType = formType;
        }

        public BootstrapFormType FormType
        {
            get { return formType; }
        }
    }
}