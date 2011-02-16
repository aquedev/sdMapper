using System;
using Xunit;
using sdMapper.Data;
using System.Collections.Generic;
using Moq;

namespace sdMapper.Tests
{
    public class ItemConverterTests
    {
        private const string BodyFieldValue = "SampleText";
        private const string VoteCountValue = "10";
        private static readonly Guid SourceItemId = new Guid();
        private Mock<IMap> _mapMock;

        /// <summary>
        /// Initializes a new instance of the ItemConverterTests class.
        /// </summary>
        public ItemConverterTests()
        {
            _mapMock = new Mock<IMap>();
        }

        [Fact]
        public void Convert_ThowsIfEntityDoesntContainPropretyIdOfTypeGuid()
        {
            Type entityType = typeof(MockEntityWithoutId);
            _mapMock.SetupGet(map => map.EntityType).Returns(entityType);
            var converter = GetConverter();
            Assert.Throws<InvalidOperationException>(
                () => converter.Convert(GetSourceItem(), _mapMock.Object));
        }

        [Fact]
        public void Convert_ReturnAnObjectOfTHeTypeDefinedByTheMap()
        {
            Type entityType = typeof(MockEntity);
            _mapMock.SetupGet(map => map.EntityType).Returns(entityType);

            var entity = GetConverter().Convert(GetSourceItem(), _mapMock.Object);

            Assert.Equal(entityType, entity.GetType());
        }

        private static ItemConverter GetConverter()
        {
            return new ItemConverter(new List<IFieldConverter>());
        }

        private static ThinItem GetSourceItem()
        {
            var source = new ThinItem();
            source.Template = new ThinItemTemplate { Id = new Guid(), Name = "MockTemplate" };
            source.Name = "MockItem";
            source.Id = SourceItemId;
            ThinField bodyField = new ThinField { Name = "Body", Value = BodyFieldValue };
            ThinField voteField = new ThinField { Name = "VoteCount", Value = VoteCountValue };
            source.Fields.Add(bodyField);
            source.Fields.Add(voteField);

            return source;
        }

        public class MockEntityMap : Map<MockEntity>
        {

            public MockEntityMap()
            {
                MapProperty(ent => ent.Body);
                MapProperty(ent => ent.Votes).To("VotesCount");
            }

            public override string TemplatePath
            {
                get { throw new NotImplementedException(); }
            }
        }

        public class MockEntityWithoutId
        {
            public string Name { get; set; }
        }

        public class MockEntity
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public string Body { get; set; }
            public int Votes { get; set; }

        }

    }
}
