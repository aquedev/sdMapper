using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lemon.Domain;
using System.Collections;

namespace Lemon.SitecoreLib.DataAccess.ValueResolvers
{
    public class ValuesListResolver : IValueResolver
    {
        private readonly IEnumerable<IValueResolver> m_resolvers;
        public ValuesListResolver (IEnumerable<IValueResolver> resolvers) 
        {
            m_resolvers = resolvers;
        }

        public bool CanResolve(Type type)
        {
            return IsGenericIList (type) && IsGenericParameterOfTypeNotAssignableToISitecoreDomainEnitity(type);
        }

        public object ResolveEntityPropertyValue(string rawValue, Type propertyType)
        {
            IList list = CreateList(propertyType);
            if (string.IsNullOrEmpty(rawValue))
                return list;

            var delimiter = new [] { SitecoreDataAccess.Settings.ValueDelimiter };
            string[] items = rawValue.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

            Type listArgumentType = ResolverHelper.GetArgumentType(propertyType);
            IValueResolver resolver = GetInnerResolver(listArgumentType);
            foreach (string item in items)
            {
                list.Add(resolver.ResolveEntityPropertyValue(item, listArgumentType));
            }

            return list;
            
        }

        public object ResolveItemFieldValue (object rawValue)
        {
            if (rawValue == null)
                return String.Empty;

            var list = (IList) rawValue;
            
            if (list.Count == 0)
                return String.Empty;

            Type argumentType = ResolverHelper.GetArgumentType(rawValue.GetType());

            IValueResolver resolver = GetInnerResolver(argumentType);
            
            var sb = new StringBuilder();

            foreach (object listItem in list)
            {
                sb.Append(resolver.ResolveItemFieldValue(listItem) + "|");
            }

            return sb.ToString().Substring(0, sb.Length - 1);
        }

        #region Helpers

        private static IList CreateList(Type propertyType)
        {
            Type argumentType = ResolverHelper.GetArgumentType (propertyType);
            Type listType = typeof(List<>).MakeGenericType(argumentType);
            return Activator.CreateInstance(listType) as IList;
        }

        private static bool IsGenericIList (Type propertyType)
        {
            return propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(IList<>);
        }
        #endregion

        private static bool IsGenericParameterOfTypeNotAssignableToISitecoreDomainEnitity(Type propertyType)
        {
            Type argumentType = ResolverHelper.GetArgumentType(propertyType);
            return !typeof(ISitecoreDomainEntity).IsAssignableFrom(argumentType);
        }

        protected IValueResolver GetInnerResolver (Type typeToResolve)
        {
            IValueResolver resolver = m_resolvers.FirstOrDefault(res =>
                                                                 !(res is DefaultValueResolver || res is ValuesListResolver) && res.CanResolve(typeToResolve));
            
            if (resolver == null)
                throw new InvalidOperationException(String.Format("Cannon find resolver for {0} and cannot process the list", typeToResolve));
            
            return resolver;
        }
    }
}
