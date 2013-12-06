namespace Foundation.Web.Paging
{
    public interface IPagingParameters
    {
        int PageSize { get; set; }
        int PageNumber { get; set; }
    }
}