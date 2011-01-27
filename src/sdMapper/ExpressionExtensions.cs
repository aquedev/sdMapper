using System;
using System.Linq.Expressions;
using System.Reflection;

namespace sdMapper
{
    public static class ExpressionExtensions
    {
        public static string GetPropertyName<T, TResult>(this Expression<Func<T, TResult>> expression)
        {
            if (expression == null)
                return String.Empty;

            return GetPropertyInfo(expression).Name;
        }

        public static string GetPropertyName<TResult>(this Expression<Func<TResult>> expression)
        {
            if (expression == null)
                return String.Empty;

            return GetPropertyInfo(expression).Name;
        }

        public static PropertyInfo GetProperty<T, TResult>(this Expression<Func<T, TResult>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            return GetPropertyInfo(expression);
        }

        public static PropertyInfo GetProperty<TResult>(this Expression<Func<TResult>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            return GetPropertyInfo(expression);
        }

        private static PropertyInfo GetPropertyInfo(LambdaExpression expression)
        {
            var propertyInfo = ((MemberExpression)expression.Body).Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
            }
            return propertyInfo;
        }
    }
}
