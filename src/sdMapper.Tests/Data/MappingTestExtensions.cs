using System;
using Xunit;
using sdMapper.Data;

namespace sdMapper.Tests.Data
{
    public static class MappingTestExtensions
    {

        public static void MapsTo(this Mapping mapping, string fieldName)
        {
            Assert.Equal(mapping.FieldName, fieldName);
        }


    }
}
