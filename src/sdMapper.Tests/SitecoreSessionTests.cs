using System;
using Xunit;
using sdMapper.Data;
using Moq;
using sdMapper.Tests.Mocks;

namespace sdMapper.Tests
{
    public class SitecoreSessionTests
    {
        Mapper _mapper = new Mapper();
        Mock<ISitecoreDataService> _dataServiceMock;
        Mock<IMapFinder> _finderMock;
        SitecoreSession _session;

        /// <summary>
        /// Initializes a new instance of the SitecoreSessionTests class.
        /// </summary>
        public SitecoreSessionTests()
        {
            _dataServiceMock = new Mock<ISitecoreDataService>();
            _finderMock = new Mock<IMapFinder>();
            _session = new SitecoreSession(_dataServiceMock.Object, _finderMock.Object);
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

        
        //want to load an object from session
        //want to convert an object from sitecoreItem
        //want to convert string field to string property
        //want to convert int field to int property
        
    }
}
