using System;

namespace Foundation.Web.Paging
{
    /// <summary>
    /// Container of the paging information as returned from FetchPaged Method. 
    /// </summary>
    public class PagingResults : IPagingResults 
    {
        private readonly int pageNumber;

        public PagingResults(int totalItems, int totalPages, int showingFrom, int showingTo, int pageSize,int pageNumber, string sort = null,
            string sortDirection = null,  Func<object, string> actiFunc = null)
        {
            this.pageNumber = pageNumber;
            TotalItems = totalItems;
            TotalPages = totalPages;
            ShowingFrom = showingFrom;
            ShowingTo = showingTo;
            PageSize = pageSize;
            Sort = sort;
            SortDirection = sortDirection;
            ActiFunc = actiFunc;
        }

        public int TotalItems { get; private set; }

        public int TotalPages { get; private set; }

        public int ShowingFrom { get; private set; }

        public int ShowingTo { get; private set; }

        public int PageSize { get; private set; }

        public string Sort { get; private set; }

        public string SortDirection { get; private set; }
        public Func<object, string> ActiFunc { get; set; }

        public int PageNumber { get; private set; }

        public Func<object, string> ActionFunc { get; private set; }
    }
}
