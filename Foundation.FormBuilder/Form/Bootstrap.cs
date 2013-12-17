using System.Web.Mvc;
using System.Web.Routing;
using Foundation.FormBuilder.Blocks;
using Foundation.FormBuilder.DynamicForm;

namespace Foundation.FormBuilder.Form
{
    public class Bootstrap<TModel>
    {
        private readonly HtmlHelper<TModel> helper;

        internal Bootstrap(HtmlHelper<TModel> helper)
        {
            this.helper = helper;
        }

        public FormContainer BeginForm(BootstrapFormType formType, object htmlAttributes)
        {
            string rawUrl = this.helper.ViewContext.HttpContext.Request.RawUrl;
            var 
            return new FormContainer(formType, rawUrl, FormMethod.Post, new RouteValueDictionary(htmlAttributes));
        }
    }
}
