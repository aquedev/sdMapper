using System;
using System.Collections.Generic;
using sdMapper.Utilities;
using System.Runtime.Serialization;

namespace sdMapper.Data
{
    
    public class ItemConverter
    {
        private readonly IList<IFieldConverter> _converters;
        public ItemConverter(IList<IFieldConverter> converters)
        {
            _converters = converters;
        }

        public T Convert<T>(ThinItem item)
        {
            var idProperty = ReflectionUtil.GetInstanceProperty(typeof(T), "Id");
            if (idProperty == null)
                throw new MapperException("Cannot map to entity that does'n have an Guid Id property");


            throw new NotImplementedException();
        }
    }
}
