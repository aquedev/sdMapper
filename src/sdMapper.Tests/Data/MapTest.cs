using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using sdMapper.Data;
using sdMapper.Data.Extensions;

namespace sdMapper.Tests.Data
{
    public class MapTest
    {
        public class NewsArticleMockMap : Map<NewsArticleMock>
        {
            public NewsArticleMockMap()
            {
                MapProperty(article => article.Title);
            }

            public override string TemplatePath
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
        }

        public class NewsArticleMock
        {
            public string Title { get; set; }
            public DateTime Date { get; set; }
            public string SubTitle { get; set; }
            public string Body { get; set; }
            public string Author { get; set; }
        }

        [Fact]
        public void Can_map_property_to_field_with_same_name()
        {
            var map = new NewsArticleMockMap();

            map.MappingFor(art => art.Title).MapsTo("Title");
        }
    }
}
