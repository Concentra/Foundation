using System;

namespace Foundation.Web.Paging
{
    public interface IPagingInfo
    {
        int TotalItems { get; set; }
        int TotalPages { get; set; }
        int ShowingFrom { get; set; }
        int ShowingTo { get; set; }
        Func<object, string> ActionFunc { get; set; }
    }
}