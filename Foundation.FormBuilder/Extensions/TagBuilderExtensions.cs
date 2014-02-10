using System.Diagnostics;
using System.Web.Mvc;

namespace Foundation.FormBuilder.Extensions
{
    public static class TagBuilderExtensions
    {
        public static MvcHtmlString ToMvcHtmlString(this TagBuilder tagBuilder, TagRenderMode renderMode)
        {
            return new MvcHtmlString(tagBuilder.ToString(renderMode));
        }
    }

}