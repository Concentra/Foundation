namespace Foundation.Web.Paging
{
    public class PagingParameters: IPagingParameters
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string Sort { get; set; }
    }
}
