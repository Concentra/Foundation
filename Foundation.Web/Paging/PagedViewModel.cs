namespace Foundation.Web.Paging
{
    public class PagedViewModel 
    {
        public PagedViewModel()
        {
            PagingAndSortingParameters = new PagingAndSortingParameters();
        }
        
        public PagingAndSortingParameters PagingAndSortingParameters { get; set; }
    }
}