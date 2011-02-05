using System;

namespace sdMapper.Data
{
    public interface IFieldConverter
    {
        bool CanConvertProperty(Type type);
        object ConvertFieldToProperty(ThinField field, Type propertyType);
        string ConvertPropertyToField(object value);
    }
}
