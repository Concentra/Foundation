namespace Foundation.Web
{
    public interface IWebConfigurator
    {
        int PageSize { get; }
        string PageTitle { get; }
    }
}