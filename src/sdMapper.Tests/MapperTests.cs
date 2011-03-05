using System;
using Xunit;
using Moq;

namespace sdMapper.Tests
{
    public class MapperTests : IUseFixture<MapperSetup>
    {
        public void SetFixture(MapperSetup data)
        {
            data.Setup();
        }

        [Fact]
        public void CreateSession_ReturnsNotNullSession()
        {
            Mapper mapper = new Mapper();
            Assert.NotNull(mapper.CreateSession());
        }

        [Fact]
        public void Initialise_SetsTheGlobalServiceResolver()
        {
            Mock<IServiceResolver> mockResolver = new Mock<IServiceResolver>();
            Mapper.Initialise(mockResolver.Object);
            Assert.Equal(mockResolver.Object, Mapper.Resolver);
        }

        [Fact]
        public void Initialise_WithNullResolver_ThorwsExeption()
        {
            Assert.Throws<ArgumentNullException>(() => Mapper.Initialise(null));
        }


        

    }
}