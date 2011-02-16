using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lemon.SitecoreLib.DataAccess.ValueResolvers
{
	public class GuidValueResolver : IValueResolver
	{
		public bool CanResolve(Type type)
		{
			return typeof (Guid) == type;
		}

		public object ResolveEntityPropertyValue(string rawValue, Type propertyType)
		{
			return new Guid(rawValue);
		}

		public object ResolveItemFieldValue(object rawValue)
		{
			return rawValue;
		}
	}
}
