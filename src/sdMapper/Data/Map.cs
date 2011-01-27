using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace sdMapper.Data
{
    public abstract class Map<TEntity> : IMap
        where TEntity : class, new()
    {
        public IList<Mapping> _mappings = new List<Mapping>();

        public abstract string TemplatePath { get; }

        public IList<Mapping> Mappings
        {
            get { return _mappings; }
            set { _mappings = value; }
        }

        protected void MapProperty<TResult>(Expression<Func<TEntity,TResult>> expression)
        {
            var builder = CreateBuilder();
            builder.MapProperty(expression);
            builder.Build();
        }

        private MappingBuilder<TEntity> CreateBuilder()
        {
            return new MappingBuilder<TEntity>(this);
        }

    }
}
