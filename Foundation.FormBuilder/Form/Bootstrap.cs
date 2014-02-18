using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using Foundation.FormBuilder.Blocks;
using Foundation.FormBuilder.BootStrapSet;
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

        #region Forms

        public FormContainer BeginForm(IDictionary<string, object> htmlAttributes)
        {
            var rawUrl = this.helper.ViewContext.HttpContext.Request.RawUrl;
            var textWriter = new HtmlTextWriter(helper.ViewContext.Writer);
            var fc = new FormContainer(textWriter, rawUrl, BootstrapFormType.Horizontal, FormMethod.Post, htmlAttributes);

            return fc;
        }

        public FormContainer BeginForm(BootstrapFormType formType)
        {
            var rawUrl = this.helper.ViewContext.HttpContext.Request.RawUrl;
            var textWriter = new HtmlTextWriter(helper.ViewContext.Writer);
            var fc = new FormContainer(textWriter, rawUrl);
            return fc;
        }

        public FormContainer BeginForm(BootstrapFormType formType, object htmlAttributes)
        {
            var rawUrl = this.helper.ViewContext.HttpContext.Request.RawUrl;
            var textWriter = new HtmlTextWriter(helper.ViewContext.Writer);
            var fc = new FormContainer(textWriter, rawUrl, method: FormMethod.Post, htmlAttributes: htmlAttributes);
            return fc;
        }


        public FormContainer BeginForm(object routeValues, BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            return BeginForm(null, null, new RouteValueDictionary(routeValues), FormMethod.Post, new RouteValueDictionary(), formType);
        }

        public FormContainer BeginForm(RouteValueDictionary routeValues, BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            return BeginForm(null, null, routeValues, FormMethod.Post, new RouteValueDictionary(), formType);
        }

        public FormContainer BeginForm(string actionName, string controllerName, BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            return BeginForm(actionName, controllerName, new RouteValueDictionary(), FormMethod.Post, new RouteValueDictionary(), formType);
        }

        public FormContainer BeginForm(string actionName, string controllerName, object routeValues, BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            return BeginForm(actionName, controllerName, new RouteValueDictionary(routeValues), FormMethod.Post, new RouteValueDictionary(), formType);
        }

        public FormContainer BeginForm(string actionName, string controllerName, FormMethod method, BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            return BeginForm(actionName, controllerName, new RouteValueDictionary(), method, new RouteValueDictionary(), formType);
        }

        public FormContainer BeginForm(string actionName, string controllerName, RouteValueDictionary routeValues, BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            return BeginForm(actionName, controllerName, routeValues, FormMethod.Post, new RouteValueDictionary(), formType);
        }

        public FormContainer BeginForm(string actionName, string controllerName, object routeValues, FormMethod method, BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            return BeginForm(actionName, controllerName, new RouteValueDictionary(routeValues), method, new RouteValueDictionary(), formType);
        }

        public FormContainer BeginForm(string actionName, string controllerName, FormMethod method, object htmlAttributes, BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            return BeginForm(actionName, controllerName, new RouteValueDictionary(), method, htmlAttributes, formType);
        }


        public FormContainer BeginForm(string actionName, string controllerName, RouteValueDictionary routeValues, FormMethod method, object htmlAttributes, BootstrapFormType formType = BootstrapFormType.Horizontal)
        {
            var formAction = UrlHelper.GenerateUrl(null, actionName, controllerName, routeValues, this.helper.RouteCollection, this.helper.ViewContext.RequestContext, true);
            var textWriter = new HtmlTextWriter(helper.ViewContext.Writer);
            var fc = new FormContainer(textWriter, formAction, method: method, htmlAttributes: htmlAttributes, formType: formType);
            return fc;
        }

        #endregion Forms
    }
}
