using System.Web.Mvc;
using System.Web.Routing;
using Foundation.FormBuilder.DynamicForm;

namespace Foundation.FormBuilder.Extensions
{
    public static class RouteValueDictionaryExtensions
    {
        public static void Merge(this RouteValueDictionary htmlAttributes, string key, string value, bool replaceExisting = false)
        {
            if (htmlAttributes.ContainsKey(key))
            {
                if (replaceExisting)
                {
                    htmlAttributes[key] = value;
                }
                else
                {
                    htmlAttributes[key] += " " + value;
                }
            }
            else
            {
                htmlAttributes.Add(key, value);
            }
        }
    }
}