using System;
using Xunit;
using Moq;

namespace sdMapper.Tests
{
    public class MapperTests : IUseFixture<MapperSetup>
    {
        private Mapper _mapper;

        public void SetFixture(MapperSetup data)
        {
            _mapper = data.Setup();
        }

        [Fact]
        public void CreateSession_ReturnsNotNullSession()
        {
            Assert.NotNull(_mapper.CreateSession());
        }

        [Fact]
        public void Initialise_WithValidParameters_ReturnNotNullMapper()
        {
            Mock<IServiceResolver> mockResolver = new Mock<IServiceResolver>();
            var mapper = Mapper.Initialise(mockResolver.Object);
            Assert.NotNull(mapper);
            Assert.IsType<Mapper>(mapper);
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