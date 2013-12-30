using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace Foundation.Web.Extensions
{
    public static class ButtonExtensions
    {
        

        public static MvcHtmlString ActionButton(this HtmlHelper helper, string text, string actionName, string controllerName, object routeValues = null, object htmlAttributes = null, string glyphIcons = null,  BootstrapNamedColor color = BootstrapNamedColor.Default)
        {
            var builder = new TagBuilder("a");
            builder.SetInnerText(text);

            builder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            switch (color)
            {
                case BootstrapNamedColor.Important:
                    builder.AddCssClass("btn btn-danger");
                    break;
                case BootstrapNamedColor.Default:
                    builder.AddCssClass("btn");
                    break;
                case BootstrapNamedColor.Info:
                    builder.AddCssClass("btn btn-info");
                    break;
                case BootstrapNamedColor.Inverse:
                    builder.AddCssClass("btn btn-inverse");
                    break;
                case BootstrapNamedColor.Primary:
                    builder.AddCssClass("btn btn-primary");
                    break;
                case BootstrapNamedColor.Success:
                    builder.AddCssClass("btn btn-success");
                    break;
                case BootstrapNamedColor.Warning:
                    builder.AddCssClass("btn btn-warning");
                    break;
                default:
                    builder.AddCssClass("btn");
                    break;
            }

            builder.AddCssClass("text-left");

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            builder.MergeAttribute("href", urlHelper.Action(actionName, controllerName, routeValues));
            if (!string.IsNullOrEmpty(glyphIcons))
            {
                var glyphiconBuilder = new TagBuilder("span");
                glyphiconBuilder.AddCssClass("pull-right glyphicon glyph" + glyphIcons);
                builder.InnerHtml = glyphiconBuilder.ToString() + " " + builder.InnerHtml;
            }

            return MvcHtmlString.Create(builder.ToString());
        }


        public static MvcHtmlString ButtonWithinGroup(this HtmlHelper helper, string text, string actionName, string controllerName, object routeValues = null, object htmlAttributes = null, string glyphIcons = null, BootstrapNamedColor color = BootstrapNamedColor.Default)
        {
            var builder = new TagBuilder("li");
            var buttonHtml = helper.ActionButton(text, actionName, controllerName, routeValues, htmlAttributes, glyphIcons, color);
            builder.InnerHtml =  buttonHtml.ToHtmlString();
            return MvcHtmlString.Create(builder.ToString());
        }

        public static MvcHtmlString Divider(this HtmlHelper helper, string text, string actionName, string controllerName, object routeValues = null, object htmlAttributes = null, string glyphIcons = null, BootstrapNamedColor color = BootstrapNamedColor.Default)
        {
            var builder = new TagBuilder("li");
            builder.AddCssClass("divider");
            return MvcHtmlString.Create(builder.ToString());
        }
    }
}
