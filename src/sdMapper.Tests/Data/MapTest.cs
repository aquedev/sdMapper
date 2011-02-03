using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using sdMapper.Data;
using sdMapper.Data.Extensions;
using System.Linq.Expressions;

namespace sdMapper.Tests.Data
{
    public class MapTest
    {
        public class NewsArticleMockMap : Map<NewsArticleMock>
        {
            public NewsArticleMockMap()
            {
                MapProperty(article => article.Title);
                MapProperty(article => article.Body).To("Text");
            }

            public override string TemplatePath
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
        }

        

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
