using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Reflection;
using sdMapper.Utilities;

namespace sdMapper.Tests
{
    public class ReflectionUtilTests
    {
        public class ReflectionTestObject
        {
            public Guid Id { get; set; }
            public string PrivateProperty { get; set; }
        }

        [Fact]
        public void GetProperty_WhenTypeIsNull_ThowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            ReflectionUtil.GetInstanceProperty(null, "property"));
        }

        [Fact]
        public void GetProperty_WhenPropertyNameIsNullOrEmpty_ThowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            ReflectionUtil.GetInstanceProperty(typeof(ReflectionTestObject), ""));
        }

        [Fact]
        public void GetProperty_WithTypeAndPropertyName_ReturnCorrectPropertyInfo()
        {
            string idPropertyName = "Id";
            PropertyInfo prop = ReflectionUtil.GetInstanceProperty(typeof(ReflectionTestObject), idPropertyName);
            Assert.Equal<PropertyInfo>(typeof(ReflectionTestObject).GetProperty(idPropertyName), prop);
        }

        [Fact]
        public void GetProperty_WithPrivatePropertyName_ReturnsCorrectPropertyInfo()
        {
            string privatePropertyName = "PrivateProperty";
            PropertyInfo prop = ReflectionUtil.GetInstanceProperty(typeof(ReflectionTestObject), privatePropertyName);
            Assert.Equal<PropertyInfo>(typeof(ReflectionTestObject).GetProperty(privatePropertyName), prop);
        }

        [Fact]
        public void GetProperty_WithNonExistantProperty_ThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => 
                ReflectionUtil.GetInstanceProperty(typeof(ReflectionTestObject), "DoesntExist")); 
        }
    }
}
