namespace Foundation.Web.Paging
{
    public class PagedViewModel 
    {
        public PagedViewModel()
        {
            PagingInfo = new PagingInfoViewModel();
        }
        
        public PagingInfoViewModel PagingInfo { get; set; }
    }
}