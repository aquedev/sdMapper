using System;
using sdMapper.Extensions;
using Xunit;

namespace sdMapper.Tests.Extensions
{
    public class ArgumentExtensionsTests
    {
        [Fact]
        public void NotNull_WithNullValue_ThrowsException()
        {
            Object argument = null;
            Assert.Throws<ArgumentException>(() => argument.NotNull("argument"));
        }

        [Fact]
        public void NotNull_WithNonNullValue_DoesntThrow()
        {
            Object argument = new object();
            Assert.DoesNotThrow(() => argument.NotNull("argument"));
        }

        [Fact]
        public void NotNull_WithNullableInt_DoesntThrow()
        {
            Nullable<int> testArg = 5;
            Assert.DoesNotThrow(() => testArg.NotNull("testArg"));
        }

        [Fact]
        public void NotNull_WithNullNullableInt_ThrowsArgumentException()
        {
            Nullable<int> testArg = null;
            Assert.Throws<ArgumentException>(() => testArg.NotNull("testArg"));
        }

        [Fact]
        public void NotNullOrEmpty_WithEmptyString_ThorwsArgumentException()
        {
            string arg = String.Empty;
            Assert.Throws<ArgumentException>(() => arg.NotNullOrEmpty("arg"));
        }

        [Fact]
        public void NotNullOrEmpty_WithNullString_ThorwsArgumentException()
        {
            string arg = String.Empty;
            Assert.Throws<ArgumentException>(() => arg.NotNullOrEmpty("arg"));
        }

        [Fact]
        public void NotNullOrEmpty_WithString_DoesntThrow()
        {
            string arg = "TestString";
            Assert.DoesNotThrow(() => arg.NotNullOrEmpty("arg"));
        }
    }
}
