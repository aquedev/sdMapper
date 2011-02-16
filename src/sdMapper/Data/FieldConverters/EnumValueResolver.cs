using System;
using System.Collections.Generic;
using System.Linq;

namespace Lemon.SitecoreLib.DataAccess.ValueResolvers
{
    public class EnumValueResolver : IValueResolver
    {
        public virtual bool CanResolve(Type type)
        {
            return type.IsEnum;
        }

        public virtual object ResolveEntityPropertyValue(string rawValue, Type propertyType)
        {
            if (IsMemberOfEnum (rawValue, propertyType))
            {
                return Enum.Parse(propertyType, rawValue, true);
            }

            return Enum.Parse(propertyType, Enum.GetNames(propertyType).First());
        }

        public object ResolveItemFieldValue(object rawValue)
        {
            return rawValue.ToString();
        }

        internal bool IsMemberOfEnum (string rawValue, Type type)
        {
            IEnumerable<string> names = Enum.GetNames(type).Select(name => name.ToLower());
            return names.Contains(rawValue.ToLower());
        }
    }
}
