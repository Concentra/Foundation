using System.Web.Mvc;

namespace VortexSoft.Bootstrap
{
    public interface IDynamicViewBuilder<TModel>
    {
        MvcHtmlString Build(TModel model);
    }
}