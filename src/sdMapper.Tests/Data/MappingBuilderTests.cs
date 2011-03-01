using System;
using Xunit;
using sdMapper.Data;
using System.Linq.Expressions;

namespace sdMapper.Tests.Data
{
    public class MappingBuilderTests
    {
        #region Test Classes
        private class MockEntity
        {
            public string Body { get; set; }
        }

        private class MockEntityMap : Map<MockEntity>
        {
            public override string TemplatePath
            {
                get { throw new NotImplementedException(); }
            }
        }
        #endregion

        Map<MockEntity> _map;
        Expression<Func<MockEntity, string>> _propertyExpression;

        /// <summary>
        /// Initializes a new instance of the MappingBuilderTests class.
        /// </summary>
        public MappingBuilderTests()
        {
            _map = new MockEntityMap();
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

        [Fact]
        public void SetFieldName_WithNewFieldName_SetsTheMappingFieldName()
        {
            string newFieldName = "NewFieldName";
            var builder = CreateBuilder();
            builder.MapProperty(_propertyExpression);
            builder.To(newFieldName);
            builder.Build();

            Assert.NotNull(_map.MappingFor(art => art.Body));
            _map.MappingFor(art => art.Body).MapsTo(newFieldName);
        }

        private MappingBuilder<MockEntity> CreateBuilder()
        {
            return new MappingBuilder<MockEntity>(_map);
        }
    }
}
