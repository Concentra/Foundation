using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Foundation.Web.Paging;

namespace Foundation.Web.Extensions
{
    public static class TableSorterExtensions
    {
        public static MvcHtmlString SortableHeader(this HtmlHelper row, IPagingInfo pagingInfo, string id, string title, object htmlAttributes = null)
        {
                return row.SortableHeader(pagingInfo.Sort, id, title, pagingInfo.ActionFunc, htmlAttributes);
        }

        public static MvcHtmlString SortableHeader(this HtmlHelper row, string currentSort, string id, string title, Func<object, string> urlActionDelegate, object htmlAttributes = null)
        {
            var properties = string.Empty;
            IDictionary<string, object> attributes = new RouteValueDictionary(htmlAttributes);
            foreach (var attr in attributes)
            {
                properties += " " + attr.Key + "=\"" + attr.Value.ToString() + "\" ";
            }
            const string sortableHeader = "SortableHeader";
            var cssClass = sortableHeader;
            string sortIcon = "";
            if (!string.IsNullOrEmpty(currentSort) && currentSort.Substring(0, currentSort.Length - 1) == id)
            {
                string sortDirection = currentSort.Substring(currentSort.Length - 1, 1);
                cssClass += " Sorted";
                if (sortDirection == "A")
                {
                    id = string.Concat(id, "D");
                    sortIcon =  GlyphIcons.SortDec;
                }
                else
                {
                    id = string.Concat(id, "A");
                    sortIcon = GlyphIcons.SortAsc;
                }
            }
            else
            {
                id = string.Concat(id, "A");    
            }

            var iconSpan = string.Format("<span class=\"glyphicon glyph{0}\"></span>", sortIcon);
            var link = BasePagingExtensions.CreatePageLink(urlActionDelegate, new {Sort = id}, title, title);
            return MvcHtmlString.Create(string.Format("<th class=\"{0}\" id=\"{1}\" {2}>{3} {4} </th>", cssClass, id, properties, link, iconSpan));
        }
    }
}
