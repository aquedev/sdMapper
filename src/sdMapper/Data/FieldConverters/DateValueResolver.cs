using System;
using Sitecore;

namespace Lemon.SitecoreLib.DataAccess.ValueResolvers
{
	public class DateValueResolver : IValueResolver
	{
        public virtual object ResolveEntityPropertyValue(string rawValue, Type propertyType)
		{
			return DateUtil.IsoDateToDateTime(rawValue);
		}

		public object ResolveItemFieldValue(object rawValue)
		{
			return DateUtil.ToIsoDate((DateTime) rawValue);
		}

        public virtual bool CanResolve(Type type)
        {
            return type == typeof(DateTime);
        }
    }
}