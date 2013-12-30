namespace Foundation.Web.Paging
{
    public static class PagingExtensions
    {
        public static ISortingParameters FillSortingParameters(this ISortingParameters destination, ISortingParameters parameters)
        {
            if (destination == null)
            {
                destination = new SortingParameters();
            }
            
            destination.Sort = parameters.Sort;
            destination.SortDirection = parameters.SortDirection;

            return destination;
        }

        public static IPagingParameters FillPagingParameters(this IPagingParameters destination, IPagingResults parameters)
        {
            if (destination == null)
            {
                destination = new PagingParameters();
            }
            
            destination.PageNumber = parameters.PageNumber;
            destination.PageSize = parameters.PageSize;
            destination.TotalItems = parameters.TotalItems;
            destination.TotalPages = parameters.TotalPages;

            return destination;
        }
    }
}
