using System;
using Xunit;
using sdMapper.Data;
using System.Collections.Generic;
using sdMapper.Tests.Data;
using Moq;

namespace sdMapper.Tests
{
    public class SitecoreSessionTests
    {
        Mapper _mapper = new Mapper();
        Mock<ISitecoreDataService> _dataServiceMock;
        SitecoreSession _session;

        /// <summary>
        /// Initializes a new instance of the SitecoreSessionTests class.
        /// </summary>
        public SitecoreSessionTests()
        {
            _dataServiceMock = new Mock<ISitecoreDataService>();
            _session = new SitecoreSession(_dataServiceMock.Object);
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


        
        //want to load an object from session
        //want to convert an object from sitecoreItem
        //want to convert string field to string property
        //want to convert int field to int property
        
    }
}
