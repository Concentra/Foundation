namespace Foundation.Web.Paging
{
    public interface IPagingParameters : INavigationParameters
    {
        int PageSize { get; set; }
        int PageNumber { get; set; }
        int TotalItems { get; set; }
        int TotalPages { get; set; }
    }
}