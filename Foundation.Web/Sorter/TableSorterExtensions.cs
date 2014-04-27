using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Foundation.Web.Extensions;
using Foundation.Web.Paging;

namespace Foundation.Web.Sorter
{
    public static class TableSorterExtensions
    {
        public static MvcHtmlString SortableHeader(this HtmlHelper row, string currentSort, string currentDirection, string columnId, string title, Func<object, string> actionFunc, object htmlAttributes = null)
        {
            var sortingInfo = new SortingParameters();
            sortingInfo.ActionFunc = actionFunc;
            sortingInfo.Sort = currentSort;
            sortingInfo.SortDirection = currentDirection;
            return row.SortableHeader(sortingInfo, columnId, title, htmlAttributes);
        }

        public static MvcHtmlString SortableHeader(this HtmlHelper row, object queryObject, string columnId, string title, object htmlAttributes = null)
        {
            var properties = string.Empty;
            IDictionary<string, object> attributes = new RouteValueDictionary(htmlAttributes);

            var sortingInfo = queryObject as ISortingParameters ?? new SortingParameters();

            var currentSort = sortingInfo.Sort;
            var currentDirection = sortingInfo.SortDirection;
            var urlActionDelegate = sortingInfo.ActionFunc;
            
            const string direction = "asc";
            var newSortDirection = direction;
            foreach (var attr in attributes)
            {
                properties += " " + attr.Key + "=\"" + attr.Value.ToString() + "\" ";
            }

            string sortableHeaderCssClass = Configurations.WebConfigurations.PagingConfigurations.SortableHeaderCssClass;
            string sortableHeader = sortableHeaderCssClass;
            var cssClass = sortableHeader;
            var sortIcon = "";

            if (!string.IsNullOrEmpty(currentSort) && currentSort == columnId)
            {
                var sorted = Configurations.WebConfigurations.PagingConfigurations.SortedHeaderCssClass;
                cssClass += string.Format(" {0}", sorted);
                if (currentDirection.ToLower().StartsWith("d"))
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
                newSortDirection = direction;
            }

            var iconSpan = string.Format("<span class=\"glyphicon glyph{0}\"></span>", sortIcon);

            sortingInfo.Sort = columnId;
            sortingInfo.SortDirection = newSortDirection;

            var link = BasePagingExtensions.CreatePageLink(urlActionDelegate,queryObject, title, title);

            sortingInfo.Sort = currentSort;
            sortingInfo.SortDirection = currentDirection;

            return MvcHtmlString.Create(string.Format("<th class=\"{0}\" id=\"{1}\" {2}>{3} {4} </th>", cssClass, columnId, properties, link, iconSpan));
        }
    }
}
