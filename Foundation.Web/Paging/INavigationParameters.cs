using System;

namespace Foundation.Web.Paging
{
    public interface INavigationParameters
    {
        Func<object, string> ActionFunc { get; set; }
    }
}