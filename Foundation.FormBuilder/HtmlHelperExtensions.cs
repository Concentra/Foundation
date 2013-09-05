using System.Web.Mvc;
using Foundation.FormBuilder;

namespace VortexSoft.Bootstrap
{
    public static class HtmlHelperExtensions
    {
        public static FormBuilder<TModel> FormBuilder<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            return new FormBuilder<TModel>(htmlHelper);
        }
    }
}