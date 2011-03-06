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
        ISitecoreSession _session;
        Mapper _mapper;
        public void SetFixture(MapperSetup data)
        {
            _mapper = data.Setup();
        }

        /// <summary>
        /// Initializes a new instance of the ConversionTests class.
        /// </summary>
        public ConversionTests()
        {
            _session = _mapper.CreateSession();
        }

        [Fact]
        public void Can_convert_items_with_boolean_values()
        {
            var entity = _session.Load<BooleanEntity>(MockSitecoreDataService.Guids.ItemWithCheckedCheckboxField);
            
            Assert.NotNull(entity);
            Assert.True(entity.BooleanProperty);
        }

    }
}
