using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lemon.SitecoreLib.DataAccess.ValueResolvers
{
    public class DefaultValueResolver : IValueResolver
    {
        public object ResolveEntityPropertyValue(string rawValue, Type propertyType)
        {
            return rawValue;
        }

        public object ResolveItemFieldValue(object rawValue)
        {
            return rawValue;
        }

        public virtual bool CanResolve(Type type)
        {
            return true;
        }

    }
}