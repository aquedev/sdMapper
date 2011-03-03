using System;
using System.Collections.Generic;
using sdMapper.Utilities;
using System.Reflection;
using System.Linq;

namespace sdMapper.Data
{
    public class ItemConverter
    {
        private readonly IList<IFieldConverter> _converters;
        public ItemConverter(IList<IFieldConverter> converters)
        {
            _converters = converters;
        }

        public object Convert(ThinItem item, IMap map)
        {
            var idProperty = ReflectionUtil.GetInstanceProperty(map.EntityType, "Id");
            if (idProperty == null)
                throw new MapperException("Cannot map to entity that does'n have an Guid Id property");

            object entity = CreateEntity(map);

            foreach (Mapping mapping in map.Mappings)
            {
                object convertedValue = GetConvertedValue(mapping.MappedProperty, item[mapping.FieldName]);
                mapping.MappedProperty.SetValue(entity, convertedValue, null);
            }
            return entity;
        }

        private static object CreateEntity(IMap map)
        {
            return Activator.CreateInstance(map.EntityType);
        }
        private object GetConvertedValue(PropertyInfo mappedProperty, ThinField fieldToBeConverted)
        {
            var mappedPropertyType = mappedProperty.PropertyType;
            var converter = GetConverter(mappedPropertyType);
            
            return converter.ConvertFieldToProperty(fieldToBeConverted, mappedPropertyType);
        }

        private IFieldConverter GetConverter(Type mappedPropertyType)
        {
            return _converters.First(conv => conv.CanConvertToType(mappedPropertyType));
        }

    }
}
