using System;
using Xunit;
using sdMapper.Tests.Mocks;

namespace sdMapper.Tests
{
    public class MapFinderTests : IUseFixture<MapperSetup>
    {
        public void SetFixture(MapperSetup data)
        {
            data.Setup();
        }

        MapFinder _finder;

        /// <summary>
        /// Initializes a new instance of the MapFinderTests class.
        /// </summary>
        public MapFinderTests()
        {
            _finder = new MapFinder();
        }

        [Fact]
        public void FindMap_WithTypeThatHasMap_ReturnsCorrectMap()
        {
            var map = _finder.FindMap<NewsArticleMock>();
            Assert.IsType<NewsArticleMockMap>(map);
        }

        [Fact]
        public void FindMap_WithTypeThatHasNoMap_ReturnsNull()
        {
            Assert.Null(_finder.FindMap<ObjectWithoutMap>());
        }
    }
}
