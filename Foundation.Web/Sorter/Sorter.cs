using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Compilation;
using Foundation.Web.Paging;
using NHibernate.Criterion;
using Expression = System.Linq.Expressions.Expression;

namespace Foundation.Web.Sorter
{
    public static class Sorter
    {
        public static IQueryable<T> ApplyOrder<T>(this IQueryable<T> source, ISortingParameters sortingInfo)
        {
            return source.ApplyOrder(sortingInfo.Sort, sortingInfo.SortDirection);
        }
        
        public static IQueryable<T> ApplyOrder<T>(this IQueryable<T> source, string columnName, string direction = "asc")
        {
            if (string.IsNullOrEmpty(columnName))
            {
                return source;
            }
            else
            {
                return source.ApplyMethod(columnName, direction.ToLower().Contains("asc") ? "OrderBy" : "OrderByDescending");    
            }
        }

        internal static IQueryable<T> ApplyMethod<T>(this IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof (T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }

            Type delegateType = typeof (Func<,>).MakeGenericType(typeof (T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof (Queryable).GetMethods().Single(
                method => method.Name == methodName
                          && method.IsGenericMethodDefinition
                          && method.GetGenericArguments().Length == 2
                          && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof (T), type)
                .Invoke(null, new object[] {source, lambda});
            return (IOrderedQueryable<T>) result;
        }

    }
}
