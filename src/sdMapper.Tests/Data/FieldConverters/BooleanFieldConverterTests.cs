using System;
using Xunit;
using sdMapper.Data.ValueConverters;
using Xunit.Extensions;
using sdMapper.Data;

namespace sdMapper.Tests.Data.FieldValueConverters
{
    public class BooleanFieldConverterTests
    {
        BooleanFieldConverter _converter;

        public BooleanFieldConverterTests()
        {
            _converter = new BooleanFieldConverter();
        }

        [Fact]
        public void CanConvert_WithBooleanType_ReturnsTrue()
        {
            Assert.True(_converter.CanConvertToType(typeof(bool)));
        }

        [Theory]
        [InlineData("true")]
        [InlineData("TRUE")]
        [InlineData("1")]
        [InlineData("yes")]
        public void ConvertFieldToProperty_WithTrueStringValue_ReturnTrue(string fieldValue)
        {
            Assert.True((bool)_converter.ConvertFieldToProperty(GetField(fieldValue), typeof(bool)));
        }

        [Theory]
        [InlineData("false")]
        [InlineData("FALSE")]
        [InlineData("0")]
        [InlineData("no")]
        public void ConvertFieldToProrpety_WithFalseStringValue_ReturnsFalse(string fieldValue)
        {
            Assert.False((bool)_converter.ConvertFieldToProperty(GetField(fieldValue), typeof(bool)));
        }

        [Theory]
        [InlineData("")]
        [InlineData("Invalid value")]
        [InlineData(null)]
        public void ConvertFieldToProrpety_WithInvalidStringValue_ReturnsFalse(string fieldValue)
        {
            Assert.False((bool)_converter.ConvertFieldToProperty(GetField(fieldValue), typeof(bool)));
        }

        [Fact]
        public void ConvertPropertyToField_WithTrue_Returns1()
        {
            Assert.Equal("1", _converter.ConvertPropertyToField(true));
        }

        [Fact]
        public void ConvertPropertyToField_WithFalse_Returns0()
        {
            Assert.Equal("0", _converter.ConvertPropertyToField(false));
        }

        private static ThinField GetField(string fieldValue)
        {
            return new ThinField { Name = "TestField", Value = fieldValue };
        }

     }
}
