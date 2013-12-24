using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Foundation.Web.CustomAttribute;
using Expression = System.Linq.Expressions.Expression;

namespace Foundation.Web.Filter
{
    public static class FilterUtility
    {
        public static IQueryable<T> ApplyFilter<T, TModel>(this IQueryable<T> source, TModel model)
        {
            var filterElements = ExtractElementsToRender(model).Where(f => f.FieldValue != null);
                
            var querytype = typeof(T);
            var arg = Expression.Parameter(querytype, "x");
            
            var leftHandExpression = CreateLambdaExpression<T, TModel>(filterElements, querytype, arg);

            var lambdaExpression = Expression.Lambda<Func<T, bool>>(leftHandExpression, arg);

            var result = source.Where(lambdaExpression);

            return result;
        }

        public static Expression CreateLambdaExpression<T, TModel>(IEnumerable<FilterElement> filterElements, Type querytype, ParameterExpression arg)
        {
            Expression expression = Expression.Constant(true);
            foreach (var filterElement in filterElements)
            {
                string[] props = filterElement.FilterSpecs.DataElement.Split('.');
                Expression propertyExpression = arg;
                var type = typeof (T);

                Expression notNullExpression = Expression.Constant(true);

                foreach (var property in  props)
                {
                    PropertyInfo pi = type.GetProperty(property);
                    propertyExpression = Expression.Property(propertyExpression, pi);
                    var nullExpression = Expression.Constant(GetNullExpressionForType(pi.PropertyType), pi.PropertyType);
                    notNullExpression = Expression.AndAlso(notNullExpression, Expression.NotEqual(propertyExpression, nullExpression));
                    type = pi.PropertyType;
                }

                var valueExpression = Expression.Constant(filterElement.FieldValue, filterElement.Property.PropertyType);
                
                BinaryExpression conditionExpression = Expression.Equal(propertyExpression, valueExpression);

                switch (filterElement.FilterSpecs.OperatorOption)
                {
                    case Operator.Equal:
                        conditionExpression = Expression.Equal(propertyExpression, valueExpression);
                        break;
                   case Operator.GreaterThan:
                        break;
                   case    Operator.GreaterThanOrEqualTo:
                        break;
                }


                var clausePart = Expression.AndAlso(notNullExpression, conditionExpression);
                
                if (!filterElement.FieldValue.Equals(GetNullExpressionForType(filterElement.Property.PropertyType)))
                {
                    expression = Expression.AndAlso(expression, clausePart);
                }
            }
            
            return expression;
        }

        private static object GetNullExpressionForType(Type type)
        {
            var canBeNull = CanBeNull(type);
            var nullValue = canBeNull ? null : Activator.CreateInstance(type);
            return nullValue;
        }

        private static bool CanBeNull(Type type)
        {
            return !(type.IsValueType || (Nullable.GetUnderlyingType(type) != null));
        }


        public static IEnumerable<FilterElement> ExtractElementsToRender<TModel>(TModel model)
        {
            Dictionary<string, PropertyInfo> properties = typeof(TModel).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                                          .ToDictionary(p => p.Name, p => p);

           var filterElements = properties.Select(p => FilterElement(model, p))
                                    .Where(p => p.FilterSpecs != null)
                                    .ToList();

           return filterElements;
        }

        private static FilterElement FilterElement<TModel>(TModel model, KeyValuePair<string, PropertyInfo> p)
        {
            return new FilterElement
            {
                FilterSpecs = p.Value.GetCustomAttributes(typeof(FilterControl), false)
                    .Cast<FilterControl>()
                    .FirstOrDefault(),
                Property = p.Value,
                FieldValue = p.Value.GetValue(model, null),
            };
        }
    }

    public class FilterElement
    {
        public FilterControl FilterSpecs { get; set; }
        public PropertyInfo Property { get; set; }
        public object FieldValue { get; set; }
    }
}
