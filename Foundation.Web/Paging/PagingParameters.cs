using System;

namespace Foundation.Web.Paging
{
    public class NavigationParameters : INavigationParameters
    {
        public Func<object, string> ActionFunc { get; set; }
    }

    public class PagingParameters: NavigationParameters, IPagingParameters
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }
        
        public int TotalItems { get; set; }
        
        public int TotalPages { get; set; }
    }

    public class SortingParameters : NavigationParameters, ISortingParameters
    {
        public string Sort { get; set; }

        public string SortDirection { get; set; }
    }

    public class PagingAndSortingParameters :NavigationParameters, IPagingParameters, ISortingParameters
    {
        public string Sort { get; set; }

        public string SortDirection { get; set; }
        
        public int PageSize { get; set; }
        
        public int PageNumber { get; set; }
        
        public int TotalItems { get; set; }
        
        public int TotalPages { get; set; }
    }



}
