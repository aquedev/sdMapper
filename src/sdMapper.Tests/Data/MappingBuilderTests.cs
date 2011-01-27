using System;
using Xunit;
using sdMapper.Data;
using System.Linq.Expressions;

namespace sdMapper.Tests.Data
{
    public class MappingBuilderTests
    {
        public class NewsArticleMockMap : Map<NewsArticleMock>
        {
            public override string TemplatePath
            {
                get { throw new NotImplementedException(); }
            }
        }

        Map<NewsArticleMock> _map;
        Expression<Func<NewsArticleMock, string>> _propertyExpression;

        /// <summary>
        /// Initializes a new instance of the MappingBuilderTests class.
        /// </summary>
        public MappingBuilderTests()
        {
            _map = new NewsArticleMockMap();
            _propertyExpression = article => article.Body;
        }

        [Fact]
        public void Build_WithoutMappingProperty_ThowsInvalidOperationException()
        {
            var builder = CreateBuilder();
            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }

        [Fact]
        public void MapProperty_CreatesMappingThatMapsPropertyToSameFieldName()
		{
		    var builder = CreateBuilder();
		    builder.MapProperty(_propertyExpression);
		    builder.Build();

            Assert.NotNull(_map.MappingFor(art => art.Body));
            _map.MappingFor(art => art.Body).MapsTo("Body");
		}

        private MappingBuilder<NewsArticleMock> CreateBuilder()
        {
            return new MappingBuilder<NewsArticleMock>(_map);
        }
    }
}
