using System;
using System.Web.Mvc;

namespace Foundation.Web.Paging
{
    [Bind(Exclude = "ActionFunc")]
    public class PagingInfoViewModel : IPagingParameters , ISortingParameters , IPagingResults
    {
        
        public int TotalItems { get; set; }

        public int TotalPages { get; set; }

        public int ShowingFrom { get; set; }

        public int ShowingTo { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string Sort { get; set; }

        public string SortDirection { get; set; }

        public Func<object, string> ActionFunc { get; set; }
    }
}
