using System;
using System.Reflection;
using sdMapper.Extensions;

namespace sdMapper.Utilities
{
    public class ReflectionUtil
    {
        public static PropertyInfo GetInstanceProperty(Type type, string propertyName)
        {
            type.NotNull("type");
            propertyName.NotNullOrEmpty("propertyName");

            var prop = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (prop == null)
                throw new InvalidOperationException(String.Format("Property '{0}' doesn't exist in '{1}'", propertyName, type.Name));

            return prop;
        }

    }
}
