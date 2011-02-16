using System;
using System.Reflection;
using Sitecore;
using Convert=System.Convert;

namespace Lemon.SitecoreLib.DataAccess.ValueResolvers
{
    public class NullableResolver : IValueResolver
    {
        public bool CanResolve(Type type)
        {
            if (!IsNullableGenericType (type))
                return false;

            Type argumentType = ResolverHelper.GetArgumentType (type);
            return argumentType.IsValueType;
        }

        public object ResolveEntityPropertyValue(string rawValue, Type propertyType)
        {
            Type argumentType = ResolverHelper.GetArgumentType (propertyType);
            Type nullableType = typeof(Nullable<>);

            if (!string.IsNullOrEmpty (rawValue))
            {
                object value = GetObjectValue (rawValue, argumentType);
                if (value != null)
                {
                    Type type = nullableType.MakeGenericType(argumentType);
                    return Activator.CreateInstance (type, value);
                }
            }

            return ResolverHelper.CreateGenericClassInstance (nullableType, argumentType);
        }

        private static object GetObjectValue (string value, Type type)
        {
            if (type.IsEnum)
            {
                return GetEnumValue (type, value);
            }

            if (type.Equals ((typeof(bool))))
            {
                return GetBooleanValue (value);
            }

            if (type.Equals (typeof(DateTime)))
            {
                return GetDateTimeValue (value);
            }

            if (typeof(IConvertible).IsAssignableFrom (type))
            {
                return GetConvertibleValue (type, value);
            }

            string message = String.Format ("Unsupported type being used with NullableResolver: [{0}]", type.Name);
            throw new Exception(message);
        }

        private static object GetConvertibleValue (Type type, string value)
        {
            try
            {
                return Convert.ChangeType (value, type);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static object GetDateTimeValue (string value)
        {
            if (DateUtil.IsIsoDate(value))
                return DateUtil.IsoDateToDateTime (value);
            return null;
        }

        private static object GetBooleanValue (string value)
        {
            switch (value.ToLower())
            {
                case "yes":
                case "on":
                case "true":
                case "1":
                    return true;

                case "no":
                case "off":
                case "false":
                case "0":
                    return false;

                default:
                    return null;
            }
        }

        private static object GetEnumValue (Type type, string value)
        {
            try
            {
                return Enum.Parse (type, value, true);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object ResolveItemFieldValue (object rawValue)
        {
            Type valueType = rawValue.GetType ();
            string valueAsString;

            if (IsNullableGenericType (valueType))
            {
                object innerValue = ExtractInnerValue (rawValue);
                if (innerValue == null)
                    return String.Empty;
                valueAsString = innerValue.ToString ();
            }
            else
            {
                valueAsString = rawValue.ToString ();
            }
            DateTime dt;
            if (DateTime.TryParse(valueAsString, out dt))
                return DateUtil.ToIsoDate (dt);
            
            return valueAsString;
        }

        private static object ExtractInnerValue(object rawValue)
        {
            Type valueType = rawValue.GetType();
            PropertyInfo hasValueProperty = valueType.GetProperty ("HasValue");
            object value = hasValueProperty.GetValue (rawValue, new object[0]);
            if (value.Equals ((true)))
            {
                PropertyInfo valueProperty = valueType.GetProperty ("Value");
                value = valueProperty.GetValue(rawValue, new object[0]);
                return value;
            }
            return null;
        }

        private static bool IsNullableGenericType (Type type)
        {
            if (!type.IsGenericType)
                return false;
            
            Type genericTypeDefinition = type.GetGenericTypeDefinition();
            return genericTypeDefinition == typeof(Nullable<>);
        }
    }
}