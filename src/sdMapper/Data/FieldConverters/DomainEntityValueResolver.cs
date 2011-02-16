using System;
using Lemon.Domain;
using Lemon.Diagnostics;

namespace Lemon.SitecoreLib.DataAccess.ValueResolvers
{
    public class DomainEntityValueResolver : IValueResolver
    {
        private static ILogger Logger
        {
            get { return DataAccessLogger.Logger; }
        }

        public object ResolveEntityPropertyValue (string rawValue, Type propertyType)
        {
            Logger.LogDebugMessage(String.Format("DomainEntityValueResolver: Resolving '{0}' to type {1}", rawValue, propertyType.FullName));
            if (!string.IsNullOrEmpty(rawValue))
            {
                var repo = new DomainEntityRepository();
                return repo.GetEntityOfType(propertyType, new Guid(rawValue));
            }
            return null;
        }

        public bool CanResolve(Type type)
        {
            return typeof(ISitecoreDomainEntity).IsAssignableFrom(type);
        }

        public object ResolveItemFieldValue (object rawValue)
        {
            var domainEntity = rawValue as ISitecoreDomainEntity;
            if (domainEntity != null)
                return domainEntity.Id.ToString("B").ToUpper();

            return String.Empty;
        }
    }
}
