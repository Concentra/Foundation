namespace Foundation.Web.Paging
{
    public class PagedViewModel 
    {
        public PagedViewModel()
        {
            PagingInformationViewModel = new PagingInfoViewModel();
        }
        
        public PagingInfoViewModel PagingInformationViewModel { get; set; }
    }
}