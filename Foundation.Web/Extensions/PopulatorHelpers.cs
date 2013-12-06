using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Linq;

namespace Foundation.Web.Extensions
{
    public static class PopulatorHelpers
    {
        public static IEnumerable<SelectListItem> CreateDropDownList<T>(this IEnumerable<T> source, Func<T, string> textPropertySelector, Func<T, object> valuePropertySelector, object value = null)
        {
            return source
                 .Select(obj => new SelectListItem
                 {
                     Text = textPropertySelector(obj),
                     Value = valuePropertySelector(obj).ToString(),
                     Selected = value != null && valuePropertySelector(obj).ToString() == value.ToString()
                 }).OrderBy(x => x.Text);
        }

        public static IEnumerable<SelectListItem> CreateDropDownList<T>(this ISession session, Func<T, string> textPropertySelector, Func<T, object> valuePropertySelector, object value = null)
        {
            return session.Query<T>()
                .AsEnumerable()
                .CreateDropDownList(textPropertySelector, valuePropertySelector, value);

        }
    }
}
