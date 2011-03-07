using System;

namespace sdMapper.Data.ValueConverters
{
	public class BooleanFieldConverter : IFieldConverter
	{
        internal bool IsValidValue (string value)
        {
            switch (value.ToLower())
            {
                case "1":
                case "yes":
                case "on":
                case "true":
                case "0":
                case "no":
                case "off":
                case "false":
                    return true;

                default:
                    return false;
            }
        }

        public object ResolveItemFieldValue(object rawValue)
        {
            return rawValue;
        }

        public bool CanConvertToType(Type type)
        {
            return type == typeof(bool);
        }

        public object ConvertFieldToProperty(ThinField field, Type propertyType)
        {
            var rawValue = field.Value ?? String.Empty; 
            switch (rawValue.ToLower())
            {
                case "1":
                case "yes":
                case "on":
                case "true":
                    return true;
                default:
                    return false;
            }
        }

        public string ConvertPropertyToField(object value)
        {
            return (bool)value ? "1" : "0";
        }

    }
}