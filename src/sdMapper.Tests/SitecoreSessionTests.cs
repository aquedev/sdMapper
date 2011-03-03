using System;
using Xunit;
using sdMapper.Data;
using Moq;
using sdMapper.Tests.Mocks;
using sdMapper.Data.FieldConverters;
using System.Collections.Generic;

namespace sdMapper.Tests
{
    public class SitecoreSessionTests : IUseFixture<MapperSetup>
    {
        public void SetFixture(MapperSetup data)
        {
            data.Setup();
        }

        Mapper _mapper = new Mapper();
        Mock<ISitecoreDataService> _dataServiceMock;
        Mock<IMapFinder> _finderMock;
        SitecoreSession _session;
        ItemConverter _converter;

        /// <summary>
        /// Initializes a new instance of the SitecoreSessionTests class.
        /// </summary>
        public SitecoreSessionTests()
        {
            _dataServiceMock = new Mock<ISitecoreDataService>();
            _finderMock = new Mock<IMapFinder>();
            _converter = new ItemConverter(new List<IFieldConverter>() { (new ConvertibleFieldConverter())});
            _session = new SitecoreSession(_dataServiceMock.Object, _finderMock.Object, _converter);
        }

        [Fact]
        public void Load_WithEmptyId_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => _session.Load<NewsArticleMock>(Guid.Empty));
        }

        [Fact]
        public void Load_WithIdThatDoesntExist_ReturnsNull()
        {
            Guid notExistantId = Guid.NewGuid();
            _dataServiceMock.Setup(service => service.GetItem(notExistantId)).Returns((ThinItem)null).Verifiable();

            _session.Load<NewsArticleMock>(notExistantId);

            _dataServiceMock.Verify();
            Assert.Null(_session.Load<NewsArticleMock>(notExistantId));
        }

        [Fact]
        public void Load_WithObjectThatDoesntHaveAMap_ThorwsInvalidOperationException()
        {
            Guid id = Guid.NewGuid();
            _dataServiceMock.Setup(service => service.GetItem(id)).Returns(new ThinItem());
            _finderMock.Setup(finder => finder.FindMap<ObjectWithoutMap>()).Returns((IMap)null);

            Assert.Throws<InvalidOperationException>(() => _session.Load<ObjectWithoutMap>(id));
        }

        [Fact]
        public void Load_WithObjectThatHasMap_ReturnsObjectOfMappedType()
        {
            Guid id = Guid.NewGuid();
            IMap map = new NewsArticleMockMap();
            _dataServiceMock.Setup(service => service.GetItem(id)).Returns(GetItem());
            _finderMock.Setup(finder => finder.FindMap<NewsArticleMock>()).Returns(map);

            NewsArticleMock entity = _session.Load<NewsArticleMock>(id);

            Assert.NotNull(entity);
            Assert.IsType(map.EntityType, entity);
        }

        [Fact]
        public void Load_WithValidIdAndType_ReturnsEntityWithCorrectTitle()
        {
            Guid id = Guid.NewGuid();
            IMap map = new NewsArticleMockMap();
            ThinItem item = GetItem();
            _dataServiceMock.Setup(service => service.GetItem(id)).Returns(item);
            _finderMock.Setup(finder => finder.FindMap<NewsArticleMock>()).Returns(map);

            NewsArticleMock entity = _session.Load<NewsArticleMock>(id);

            Assert.Equal(item["Title"].Value, entity.Title);
        }
        //want to load an object from session
        //want to convert an object from sitecoreItem
        //want to convert string field to string property
        //want to convert int field to int property

        private static ThinItem GetItem()
        {
            return new ThinItemBuilder()
                .AddField("Title", "title")
                .AddField("Text", "body")
                .AddField("SubTitle", "subTitle")
                .Build();
        }
    }
}
