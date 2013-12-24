using Foundation.Configuration;

namespace Foundation.Web.Configurations
{
    public static class WebConfigurations
    {
        public static PagingConfigurations PagingConfigurations { get; set; }

        static WebConfigurations()
        {
            PagingConfigurations = new PagingConfigurations
            {
                PaginationCssClass = "pagination",
                ActivePageClass = "active",
                FirstPageText = "awal",
                LastPageText = "akheer",
                NextPageText = "taaly",
                PreviousPageText = "sabeq",
                SortableHeaderCssClass = "sortableheader",
                SortedHeaderCssClass = "Sorted",
                SortedIcondAscending = GlyphIcons.ChevronUp,
                SortedIcondDescending = GlyphIcons.ChevronDown
            };
        }
    }
}