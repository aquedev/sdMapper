using System;
 using System.Linq;

namespace sdMapper.Data.FieldConverters
{
    public class ConvertibleFieldConverter : IFieldConverter
    {
        private readonly Type[] ExcludedTypes = { typeof(DateTime), typeof(Boolean) };

        public bool CanConvertToType(Type type)
        {
            // Can resolve any type which implements IConvertible, except DateTime, 
            // Boolean or Enum, which are more explicitly handled by other field 
            // converters
            if (ExcludedTypes.Contains(type) || type.IsEnum)
            {
                return false;
            }

            return typeof(IConvertible).IsAssignableFrom(type);
        }

        public object ConvertFieldToProperty(ThinField field, Type propertyType)
        {
            return Convert.ChangeType(field.Value, propertyType);
        }

        public string ConvertPropertyToField(object value)
        {
            return value.ToString();
        }

    }

}