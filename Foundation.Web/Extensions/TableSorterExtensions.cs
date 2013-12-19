using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Foundation.Web.Paging;

namespace Foundation.Web.Extensions
{
    public static class TableSorterExtensions
    {
        public static MvcHtmlString SortableHeader(this HtmlHelper row, ISortingParameters sortingInfo, string columnId, string title, object htmlAttributes = null)
        {
                return row.SortableHeader(sortingInfo.Sort, sortingInfo.SortDirection, columnId, title, sortingInfo.ActionFunc, htmlAttributes);
        }

        public static MvcHtmlString SortableHeader(this HtmlHelper row, string currentSort, string sortDirection, string columnId, string title, Func<object, string> urlActionDelegate, object htmlAttributes = null)
        {
            var properties = string.Empty;
            IDictionary<string, object> attributes = new RouteValueDictionary(htmlAttributes);

            var newSortDirection = "asc";
            foreach (var attr in attributes)
            {
                properties += " " + attr.Key + "=\"" + attr.Value.ToString() + "\" ";
            }
            
            const string sortableHeader = "SortableHeader";
            var cssClass = sortableHeader;
            string sortIcon = "";
            if (!string.IsNullOrEmpty(currentSort) && currentSort == columnId)
            {
                cssClass += " Sorted";
                if (sortDirection.ToLower().StartsWith("d"))
                {
                    sortIcon = GlyphIcons.ChevronDown;
                    newSortDirection = "asc";
                }
                else
                {
                    sortIcon = GlyphIcons.ChevronUp;
                    newSortDirection = "desc";
                }
            }
            else
            {
                // default (initial sort) is ascending
                sortDirection = "asc";
            }

            var iconSpan = string.Format("<span class=\"glyphicon glyph{0}\"></span>", sortIcon);

            var link = BasePagingExtensions.CreatePageLink(urlActionDelegate, new { Sort = columnId, SortDirection = newSortDirection }, title, title);
            
            return MvcHtmlString.Create(string.Format("<th class=\"{0}\" id=\"{1}\" {2}>{3} {4} </th>", cssClass, columnId, properties, link, iconSpan));
        }
    }
}
