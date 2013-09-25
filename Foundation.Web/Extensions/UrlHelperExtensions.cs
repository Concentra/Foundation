using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Foundation.Web.Extensions
{
    /// <summary>
    /// public string Action(string actionName, object routeValues);
    /// public string Action(string actionName, string controllerName
    ///                    , object routeValues);
    /// public string Action(string actionName, string controllerName
    ///                    , object routeValues, string protocol);
    ///
    /// public string RouteUrl(object routeValues);
    /// public string RouteUrl(string routeName, object routeValues);
    /// public string RouteUrl(string routeName, object routeValues
    ///                      , string protocol);
    /// </summary>
    public static class UrlHelperExtensions
    {
        public static string Action(this UrlHelper helper, string actionName
            , object routeValues, bool onlyBoundValues)
        {
            var finalRouteValues =
                new RouteValueDictionary(routeValues);

            if (onlyBoundValues)
            {
                RemoveUnboundValues(finalRouteValues, routeValues);
            }

            // Internally, MVC calls an overload of GenerateUrl with
            // hard-coded defaults.  Since we shouldn't know what these
            // defaults are, we call the non-extension equivalents.
            return helper.Action(actionName, routeValues);
        }

        public static string Action(this UrlHelper helper, string actionName
            , string controllerName, object routeValues
            , bool onlyBoundValues)
        {
            var finalRouteValues =
                new RouteValueDictionary(routeValues);

            if (onlyBoundValues)
            {
                RemoveUnboundValues(finalRouteValues, routeValues);
            }

            return helper.Action(actionName, controllerName, finalRouteValues);
        }

        public static string Action(this UrlHelper helper, string actionName
            , string controllerName, object routeValues
            , string protocol, bool onlyBoundValues)
        {
            var finalRouteValues =
                new RouteValueDictionary(routeValues);

            if (onlyBoundValues)
            {
                RemoveUnboundValues(finalRouteValues, routeValues);
            }

            return helper.Action(actionName, controllerName
                , finalRouteValues, protocol);
        }

        public static string RouteUrl(this UrlHelper helper, object routeValues
            , bool onlyBoundValues)
        {
            var finalRouteValues =
                new RouteValueDictionary(routeValues);
            if (onlyBoundValues)
            {
                RemoveUnboundValues(finalRouteValues, routeValues);
            }
            return helper.RouteUrl(finalRouteValues);
        }

        public static string RouteUrl(this UrlHelper helper, string routeName
            , object routeValues, bool onlyBoundValues)
        {
            var finalRouteValues =
                new RouteValueDictionary(routeValues);
            if (onlyBoundValues)
            {
                RemoveUnboundValues(finalRouteValues, routeValues);
            }
            return helper.RouteUrl(routeName, finalRouteValues);
        }

        public static string RouteUrl(this UrlHelper helper, string routeName
            , object routeValues, string protocol
            , bool onlyBoundValues)
        {
            var finalRouteValues =
                new RouteValueDictionary(routeValues);

            if (onlyBoundValues)
            {
                RemoveUnboundValues(finalRouteValues, routeValues);
            }

            return helper.RouteUrl(routeName, finalRouteValues, protocol);
        }

        /// <summary>
        /// Reflect into the routeValueObject and remove any keys from
        /// routeValues that are not bound by the Bind attribute
        /// </summary>
        private static void RemoveUnboundValues(RouteValueDictionary routeValues
            , object source)
        {
            if (source == null)
            {
                return;
            }

            var type = source.GetType();

            BindAttribute b = type.GetCustomAttributes(true).OfType<BindAttribute>().FirstOrDefault();

            if (b == null)
            {
                return;
            }

            foreach (var property in type.GetProperties())
            {
                var propertyName = property.Name;
                if (!b.IsPropertyAllowed(propertyName))
                {
                    routeValues.Remove(propertyName);
                }
            }
        }
    }
}