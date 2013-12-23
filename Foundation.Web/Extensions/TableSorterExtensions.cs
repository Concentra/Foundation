using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Foundation.Configuration;
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

            const string direction = "asc";
            var newSortDirection = direction;
            foreach (var attr in attributes)
            {
                properties += " " + attr.Key + "=\"" + attr.Value.ToString() + "\" ";
            }

            string sortableHeaderCssClass = Configurations.WebConfigurations.PagingConfigurations.SortableHeaderCssClass;
            string sortableHeader = sortableHeaderCssClass;
            var cssClass = sortableHeader;
            string sortIcon = "";
            if (!string.IsNullOrEmpty(currentSort) && currentSort == columnId)
            {
                var sorted = Configurations.WebConfigurations.PagingConfigurations.SortedHeaderCssClass;
                cssClass += string.Format(" {0}", sorted);
                if (sortDirection.ToLower().StartsWith("d"))
                {
                    string sortedIcondDescending = Configurations.WebConfigurations.PagingConfigurations.SortedIcondDescending;
                    sortIcon = sortedIcondDescending;
                    newSortDirection = direction;
                }
                else
                {
                    string sortedIcondAscending = Configurations.WebConfigurations.PagingConfigurations.SortedIcondAscending;
                    sortIcon = sortedIcondAscending;
                    newSortDirection = "desc";
                }
            }
            else
            {
                // default (initial sort) is ascending
                sortDirection = direction;
            }

            var iconSpan = string.Format("<span class=\"glyphicon glyph{0}\"></span>", sortIcon);

            var link = BasePagingExtensions.CreatePageLink(urlActionDelegate, new { Sort = columnId, SortDirection = newSortDirection }, title, title);
            
            return MvcHtmlString.Create(string.Format("<th class=\"{0}\" id=\"{1}\" {2}>{3} {4} </th>", cssClass, columnId, properties, link, iconSpan));
        }
    }
}
