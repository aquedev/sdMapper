using System;
using System.Linq.Expressions;
using System.Linq;

namespace sdMapper.Data
{
    public static class MapTestExtensions
    {
        public static Mapping MappingFor<TEntity, TResult>(this Map<TEntity> map, Expression<Func<TEntity,TResult>> expression) where TEntity : class, new()
        {
            var mappedProperty = expression.GetProperty();
            return map.Mappings.FirstOrDefault(mapping => mapping.MappedProperty == mappedProperty);
        }

        
    }
}
