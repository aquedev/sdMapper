using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace sdMapper.Data
{
    public abstract class Map<TEntity> : IMap
        where TEntity : class, new()
    {
        private IList<Mapping> _mappings = new List<Mapping>();

        public abstract string TemplatePath { get; }

        public IList<Mapping> Mappings
        {
            get { return _mappings; }
            set { _mappings = value; }
        }

        protected IMappingBuilder MapProperty<TResult>(Expression<Func<TEntity,TResult>> expression)
        {
            throw new NotImplementedException();
        }

    }
}
