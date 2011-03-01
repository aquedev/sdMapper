using System;
using Xunit;
using sdMapper.Data;
using sdMapper.Tests.Mocks;

namespace sdMapper.Tests.Data
{
    public class MapTest
    {
        

        [Fact]
        public void MapProperty_WithoutSpecifiedFieldName_MapsThePropertyToAFieldWithSameName()
        {
            var map = new NewsArticleMockMap();

            map.MappingFor(art => art.Title).MapsTo("Title");
        }

        [Fact]
        public void MapProperty_WithPropertyAndSpecifiedFieldName_MapsThePropertyToThatField()
        {
            var map = new NewsArticleMockMap();
            map.MappingFor(art => art.Body).MapsTo("Text");
        }
    }
}
