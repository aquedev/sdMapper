using System;
using sdMapper.Data;

namespace sdMapper
{
    public interface ISitecoreDataWrapper
    {
        ThinItem GetItem(Guid id);
        ThinItem GetItem(string path);
    }
}
