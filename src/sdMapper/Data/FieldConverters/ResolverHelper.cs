using System;

namespace Lemon.SitecoreLib.DataAccess.ValueResolvers
{
    public static class ResolverHelper
    {
        public static Type GetArgumentType (Type type)
        {
            CheckIsGenericType(type, "GetArgumentType");
            return type.GetGenericArguments()[0];
        }

        public static bool IsNullableType (Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition ().Equals (typeof (Nullable<>));
        }

        public static object CreateGenericClassInstance (Type genericType, Type argumentType)
        {
            CheckIsGenericType(genericType, "CreateGenericClassInstance<T>");
            Type type = genericType.MakeGenericType(argumentType);
            return Activator.CreateInstance(type);
        }

        private static void CheckIsGenericType (Type type, string methodName)
        {
            if (!type.IsGenericType)
            {
                string message = string.Format("{0}() called with invalid Type ({1})",
                                               methodName, type.FullName);
                throw new Exception(message);
            }
        }
    }
}