using System;
using System.Linq.Expressions;

namespace sdMapper.Data
{
    public static class MapTestExtensions
    {
        public static Mapping MappingFor<TEntity, TResult>(this Map<TEntity> map, Expression<Func<TEntity,TResult>> expression) where TEntity : class, new()
        {
            throw new NotImplementedException();
        }

        
    }
}
