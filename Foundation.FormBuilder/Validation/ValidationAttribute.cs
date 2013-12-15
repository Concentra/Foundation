using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Foundation.FormBuilder.Validation
{
    public static class ValidationUtility
    {
        public static IDictionary<string, object> GetValidationAttributes<TModel>(this HtmlHelper<TModel> html, string memberName)
        {
            var propertyExpression = ExpressionHelper.GetExpressionText(memberName);
            var metadata = ModelMetadata.FromStringExpression(propertyExpression, html.ViewData);
            var attributes = html.GetUnobtrusiveValidationAttributes(memberName, metadata);
            return attributes;
        }
    }
}
