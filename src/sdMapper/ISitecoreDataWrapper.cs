using System;
using sdMapper.Data;

namespace sdMapper
{
    public interface ISitecoreDataService
    {
        ThinItem GetItem(Guid id);
        ThinItem GetItem(string path);
    }
}
