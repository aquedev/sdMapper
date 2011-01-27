using System;
using System.Reflection;

namespace sdMapper.Data
{
    public class Mapping
    {
        public PropertyInfo MappedProperty { get; set; }
        public string FieldName { get; set; }

        public Mapping()
        {

        }
    }
}
