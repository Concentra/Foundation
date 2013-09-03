using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace Foundation.Web.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Retrieve the descriptions on the enum type.
        /// Then when you pass in the enum type (e.g. Enums.UserTitles), it will retrieve the descriptions for all the individual enums
        /// </summary>
        /// <param name="enumType">The Enumeration Type</param>
        /// <returns>A string list representing all the friendly names</returns>
        public static List<string> GetDescriptions(Type enumType)
        {
            MemberInfo[] memInfo = enumType.GetMembers(BindingFlags.Public | BindingFlags.Static)
                .Where(m => m.MemberType == MemberTypes.Field)
                .ToArray();
            var result = new List<string>();
            if (memInfo != null && memInfo.Length > 0)
            {
                foreach (MemberInfo member in memInfo)
                {
                    object[] attrs = member.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (attrs != null && attrs.Length > 0)
                    {
                        result.Add(((DescriptionAttribute)attrs[0]).Description);
                    }
                    else
                    {
                        result.Add(member.Name);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets a list of SelectListItem with text as description of the enum and value as its name.
        /// </summary>
        /// <param name="type">The Enumeration Type</param>
        /// <returns>IEnumerable of SelectListItem</returns>
        public static IEnumerable<SelectListItem> ToSelectListWithNames(this Type type)
        {
            MemberInfo[] memInfo = type.GetMembers(BindingFlags.Public | BindingFlags.Static)
                .Where(m => m.MemberType == MemberTypes.Field)
                .ToArray();
            var result = new List<SelectListItem>();
            if (memInfo != null && memInfo.Length > 0)
            {
                foreach (MemberInfo member in memInfo)
                {
                    object[] attrs = member.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (attrs != null && attrs.Length > 0)
                    {
                        result.Add(new SelectListItem() { Text = ((DescriptionAttribute)attrs[0]).Description, Value = member.Name });
                    }
                    else
                    {
                        result.Add(new SelectListItem() { Text = member.Name, Value = member.Name });
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets a description attribute of an enum by its value
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <returns>Description string</returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null &&
                attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return value.ToString();
        }
    }
}
