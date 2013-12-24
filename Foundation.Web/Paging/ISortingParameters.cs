using System;

namespace Foundation.Web.Paging
{
    public interface ISortingParameters : INavigationParameters
    {
        string Sort { get; set; }
        string SortDirection { get; set; }
    }
}