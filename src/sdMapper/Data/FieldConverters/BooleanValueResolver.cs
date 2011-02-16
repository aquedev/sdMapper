using System;

namespace sdMapper.Data.ValueConverters
{
	public class BooleanValueConverter : IValueResolver
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

	    public object ResolveEntityPropertyValue(string rawValue, Type propertyType)
        {
			if (!IsValidValue (rawValue))
			    return false;

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

	    public bool CanResolve(Type type)
        {
            return type == typeof(bool);
        }

        public object ResolveItemFieldValue(object rawValue)
        {
            return rawValue;
        }
    }
}