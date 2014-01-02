using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Foundation.FormBuilder
{
    public static class ModelCache
    {
        private static readonly Dictionary<Type, List<PropertyInfo>> propertyDictionary = new Dictionary<Type, List<PropertyInfo>>();

        public static List<PropertyInfo> GetCachedProperties(this Type type , BindingFlags bindingFlags )
        {
            List<PropertyInfo> properties;
            if (propertyDictionary.TryGetValue(type, out properties) == false)
            {
                properties = type.GetProperties(bindingFlags).ToList();
                propertyDictionary.Add(type, properties);
            }

            return properties;
        }

    }
}
