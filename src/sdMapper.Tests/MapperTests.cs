using System;
using Xunit;

namespace sdMapper.Tests
{
    public class MapperTests
    {
        [Fact]
        public void CreateSession_ReturnsNotNullSession()
        {
            Mapper mapper = new Mapper();
            Assert.NotNull(mapper.CreateSession());
        }
    }
}