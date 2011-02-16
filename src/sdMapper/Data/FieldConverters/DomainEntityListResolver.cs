using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lemon.Extensions;
using Lemon.Domain;
using System.Collections;
using Lemon.Common;
using Lemon.DataAccess;

namespace Lemon.SitecoreLib.DataAccess.ValueResolvers
{
    public class DomainEntityListResolver : IValueResolver
    {
        private IDomainEntityRepository m_repository;
        private ISitecoreDataAccessSettings m_settings;
        private ISitecoreDataAccessSettings Settings
        {
            get
            {
                if (m_settings == null)
                    return SitecoreDataAccess.Settings;
                return m_settings;
            }
        }
        
        public DomainEntityListResolver(IDomainEntityRepository repository, ISitecoreDataAccessSettings settings)
        {
            m_settings = settings;
            m_repository = repository;
        }

        public DomainEntityListResolver(IDomainEntityRepository repository) : this(repository, null)
        {
            
        }
        #region IValueResolver Members

        public bool CanResolve(Type type)
        {
            return IsGenericListOfEnities(type);
        }

        public object ResolveEntityPropertyValue(string rawValue, Type propertyType)
        {
            Type genericParameterType = propertyType.GetGenericArguments()[0];

            if (rawValue.IsNullOrEmpty())
                return CreateEmptyTypedList(genericParameterType);

            return CreateLazyList(genericParameterType, () => {
                IList list = CreateEmptyTypedList(genericParameterType);
                var delimiter = new[] { Settings.ValueDelimiter };
                string[] itemIds = rawValue.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

                IMap listTypeMap = MapFinder.FindMap(genericParameterType);
                foreach (
                    ISitecoreDomainEntity entity in
                        m_repository.GetEntities(itemIds.Select(itemId => new Guid(itemId)),
                                                        listTypeMap))
                {
                    list.Add(entity);
                }
                return list;
            });
        }

        public object ResolveItemFieldValue(object rawValue)
        {
            throw new NotImplementedException();
        }

        #endregion

        private static IList CreateEmptyTypedList(Type argType)
        {
            return Activator.CreateInstance(typeof(List<>).MakeGenericType(argType)) as IList;
        }

        private static IList CreateLazyList(Type genericParameterType, Func<IEnumerable> loader)
        {
            return Activator.CreateInstance(typeof(LazyList<>).MakeGenericType(genericParameterType), loader) as IList;
        }

        private static bool IsGenericListOfEnities(Type propertyType)
        {
            return propertyType.IsGenericType
                   && IsGenericIList(propertyType)
                   && IsGenericParameterOfTypeISitecoreDomainEnitity(propertyType);
        }

        private static bool IsGenericParameterOfTypeISitecoreDomainEnitity(Type propertyType)
        {
            return typeof(ISitecoreDomainEntity).IsAssignableFrom(propertyType.GetGenericArguments()[0]);
        }

        private static bool IsGenericIList(Type propertyType)
        {
            return propertyType.GetGenericTypeDefinition() == typeof(IList<>);
        }
    }
}
