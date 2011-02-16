using System;

namespace sdMapper.Data
{
    public interface IFieldConverter
    {
        bool CanConvertToType(Type type);
        object ConvertFieldToProperty(ThinField field, Type propertyType);
        string ConvertPropertyToField(object value);
    }
}
