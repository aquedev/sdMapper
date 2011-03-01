using System;
using Xunit;
using sdMapper.Data;
using System.Collections.Generic;
using sdMapper.Tests.Data;

namespace sdMapper.Tests
{
    public class SitecoreSessionTests
    {
        Mapper _mapper = new Mapper();

        SitecoreSession _session;

        /// <summary>
        /// Initializes a new instance of the SitecoreSessionTests class.
        /// </summary>
        public SitecoreSessionTests()
        {
            _session = new SitecoreSession();
        }

        [Fact]
        public void Load_WithEmptyId_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => _session.Load<NewsArticleMock>(Guid.Empty));
        }

        //[Fact]
        //public void Load_WithIdThatDoesntExist_ReturnsNull()
        //{
        //    Guid notExistantId = new Guid();
        //    Assert.Null(_session.Load<NewsArticleMock>(notExistantId));
        //}
        
        //want to load an object from session
        //want to convert an object from sitecoreItem
        //want to convert string field to string property
        //want to convert int field to int property
        
    }
}
