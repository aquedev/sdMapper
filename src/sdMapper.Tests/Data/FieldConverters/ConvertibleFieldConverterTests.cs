using System;
using Xunit;
using sdMapper.Data.FieldConverters;
using sdMapper.Data;
using System.Collections.Generic;


namespace sdMapper.Tests.Data.FieldValueConverters
{
    public class ConvertibleFieldConverterTests
    {
        private class TestData
        {
            public Type Type { get; set; }
            public string Value { get; set; }
            public object ConvertedValue { get; set; }

            public TestData(Type type, object convertedValue)
            {
                Type = type;
                ConvertedValue = convertedValue;
                Value = ConvertedValue.ToString();
            }
        }

        private enum TestEnum { }
        private readonly ConvertibleFieldConverter _fieldConverter;
        Type[] allowedTypes = { typeof(SByte),
                                  typeof(Byte),
                                  typeof(Int16),
                                  typeof(UInt16),
                                  typeof(Int32),
                                  typeof(UInt32), 
                                  typeof(Int64),
                                  typeof(UInt64), 
                                  typeof(Single), 
                                  typeof(Double), 
                                  typeof(Decimal), 
                                  typeof(Char), 
                                  typeof(String)};
        Type[] excludedTypes = { typeof(DateTime), typeof(Boolean), typeof(TestEnum) };
        List<TestData> _testData;


        public ConvertibleFieldConverterTests()
        {
            _fieldConverter = new ConvertibleFieldConverter();
            _testData = new List<TestData> { 
                new TestData(typeof(SByte), (SByte)8),
                new TestData(typeof(Byte), (Byte)8),
                new TestData(typeof(Int16), (Int16)1000),
                new TestData(typeof(UInt16), (UInt16)8),
                new TestData(typeof(Int32), 8),
                new TestData(typeof(UInt32), (UInt32)80),
                new TestData(typeof(Int64), (Int64)8888888),
                new TestData(typeof(Single), (Single)8.8),
                new TestData(typeof(Double), (Double)8.333),
                new TestData(typeof(Decimal), (Decimal)0.8),
                new TestData(typeof(Char), '8'),
                new TestData(typeof(String), "Test string")
            };
        }

        [Fact]
        public void CanConvert_WithAllowedTypes_ReturnsTrue()
        {
            foreach (var type in allowedTypes)
            {
                Assert.True(_fieldConverter.CanConvertToType(type), String.Format("Should be able to convert '{0}'", type) );
            }
        }

        [Fact]
        public void CanConvertToType_WithExcludedTypes_ReturnsFalse()
        {
            foreach (var type in excludedTypes)
                Assert.False(_fieldConverter.CanConvertToType(type), String.Format("Should not be able to convert '{0}'", type));
        }

        [Fact]
        public void ConvertFieldToProperty_WithTestData_ReturnsTheCorrectTypes()
        {
            foreach (var data in _testData)
            {
                ThinField field = new ThinField { Value = data.Value };
                Assert.Equal(data.ConvertedValue, 
                    _fieldConverter.ConvertFieldToProperty(field, data.Type));
            }
        }

        [Fact]
        public void ConvertPropertyToField_WithTestData_ReturnsTheCorrectStringValues()
        {
            foreach (var data in _testData)
            {
                Assert.Equal(data.Value,
                    _fieldConverter.ConvertPropertyToField(data.ConvertedValue));
            }
        }
    }
}