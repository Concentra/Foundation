using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace Foundation.Web.Extensions
{
    public static class TableSorterExtensions
    {
        public static MvcHtmlString SortableHeader(this HtmlHelper row, string currentSort, string id, string title, object htmlAttributes = null)
        {
            // produces output of the form: <th class="header" id="surnameA">Name</th>
            var properties = string.Empty;
            IDictionary<string, object> attributes = new RouteValueDictionary(htmlAttributes);
            foreach (var attr in attributes)
            {
                properties += " " + attr.Key + "=\"" + attr.Value.ToString() + "\" ";
            }

            string thclass = "header";
            if (!string.IsNullOrEmpty(currentSort) && currentSort.Substring(0, currentSort.Length - 1) == id)
            {
                string sortDirection = currentSort.Substring(currentSort.Length - 1, 1);

                if (sortDirection == "A")
                {
                    id = string.Concat(id, "D");
                    thclass += " headerSortDown";
                }
                else
                {
                    id = string.Concat(id, "A");
                    thclass += " headerSortUp";
                }
            }
            else
            {
                id = string.Concat(id, "A");    
            }

            return MvcHtmlString.Create(string.Format("<th class=\"{0}\" id=\"{1}\" {2}>{3}</th>", thclass, id, properties, title));
        }
    }
}
