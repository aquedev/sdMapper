using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using sdMapper.Tests.Mocks.Enitities;

namespace sdMapper.Tests
{
    public class ConversionTests : IUseFixture<MapperSetup>
    {
        Mapper _mapper;
        public void SetFixture(MapperSetup data)
        {
            _mapper = data.Setup();
        }

        [Fact]
        public void Can_convert_items_with_boolean_values()
        {
            var entity = GetSession().Load<BooleanEntity>(MockSitecoreDataService.Guids.ItemWithCheckedCheckboxField);
            
            Assert.NotNull(entity);
            Assert.True(entity.BooleanProperty);
        }

        private ISitecoreSession GetSession()
        {
            return _mapper.CreateSession();
        }
    }
}
