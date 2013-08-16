using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Foundation.Web.Extensions
{
    public static class TBExtensions
    {
        public static MvcHtmlString TBDropDown(this HtmlHelper htmlHelper, IDictionary<string, string> values, object htmlAttributes = null)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<td class='reportItemClass'>");
            sb.AppendLine("</td>");
            return new MvcHtmlString(sb.ToString());
        }
    }
}
