using System;
using sdMapper.Data;

namespace sdMapper
{
    public interface IMapFinder
    {
        IMap FindMap<T>() where T : class;
    }
}
