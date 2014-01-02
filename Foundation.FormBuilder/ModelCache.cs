using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.FormBuilder
{
    internal static class ModelCache
    {
        private static readonly Dictionary<Type, List<PropertyInfo>> PropertyDictionary = new Dictionary<Type, List<PropertyInfo>>();

        public static List<PropertyInfo> GetCachedProperties(this Type type , BindingFlags bindingFlags )
        {
            List<PropertyInfo> properties;
            if (PropertyDictionary.TryGetValue(type, out properties) == false)
            {
                properties = type.GetProperties(bindingFlags).ToList();
                PropertyDictionary.Add(type, properties);
            }

            return properties;
        }

    }
}
