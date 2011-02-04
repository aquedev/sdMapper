using System;
using sdMapper.Data;
using Xunit;

namespace sdMapper.Tests
{
    public class ThinItemTests
    {

        [Fact]
        public void Fields_IsEmptyForNewThinItem()
        {
            var item = new ThinItem();
            Assert.Empty(item.Fields);
        }
    }
}
