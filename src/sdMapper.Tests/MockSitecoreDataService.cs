using System;
using sdMapper.Data;

namespace sdMapper.Tests
{
    public class MockSitecoreDataService : ISitecoreDataService
    {
        public ThinItem GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public ThinItem GetItem(string path)
        {
            throw new NotImplementedException();
        }
    }
}
