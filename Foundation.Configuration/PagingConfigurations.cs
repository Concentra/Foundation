namespace Foundation.Configuration
{
    public class PagingConfigurations
    {
        /// <summary>
        /// The Css Class to be assigned to the <UL> element of the pager.
        /// </summary>
        public string PaginationCssClass { get; set; }
        
        /// <summary>
        /// Text for the "First" page link in the pager
        /// </summary>
        public string FirstPageText { get; set; }

        /// <summary>
        /// Text for the "Previous" page link in the pager
        /// </summary>
        public string PreviousPageText { get; set; }

        /// <summary>
        /// Class to be assigned to the current page emelemnt ( active).
        /// </summary>
        public string ActivePageClass { get; set; }

        /// <summary>
        /// Text for the "Next" page link in the pager
        /// </summary>
        public string NextPageText { get; set; }
        
        /// <summary>
        /// Text for the "Last" page link in the pager
        /// </summary>
        public string LastPageText { get; set; }

        /// <summary>
        /// Css Class assigned to a sortable table header <TH> tag.
        /// </summary>
        public string SortableHeaderCssClass { get; set; }

        /// <summary>
        /// Css Class assigned to a the currently sorted by  header <TH> tag.
        /// </summary>
        public string SortedHeaderCssClass { get; set; }

        /// the Glyphicon name to be used when a column is sorted descending..
        public string SortedIcondDescending { get; set; }

        /// the Glyphicon name to be used when a column is sorted ascending..
        public string SortedIcondAscending { get; set; }
    }
}
