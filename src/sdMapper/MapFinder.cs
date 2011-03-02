using System;
using sdMapper.Data;

namespace sdMapper
{
    public class MapFinder : IMapFinder
    {
        public IServiceResolver Resolver
        {
            get { return Mapper.Resolver; }
        }

        public IMap FindMap<T>()
                where T : class
        {
            return Resolver.TryResolve<Map<T>>() as IMap;
        }
    }
}
