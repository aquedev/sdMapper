using System;
using System.Linq.Expressions;

namespace sdMapper.Data
{
    public class MappingBuilder<TEntity> where TEntity : class
    {
        private readonly Map<TEntity> _map;
        public Mapping Mapping { get; set; }

        public MappingBuilder(Map<TEntity> map)
        {
            _map = map;
        }

        public MappingBuilder<TEntity> MapProperty<TResult>(Expression<Func<TEntity, TResult>> propertyExpression)
        {
            var property = propertyExpression.GetProperty();
            Mapping = new Mapping() { MappedProperty = property, FieldName = property.Name };
            return this;
        }

        public MappingBuilder<TEntity> To(string fieldName)
        {
            Mapping.FieldName = fieldName;
            return this;
        }

        public MappingBuilder<TEntity> Build()
        {
            if (Mapping == null)
                throw new InvalidOperationException("You must at least call MapProperty before Build");

            for (int i = 0; i < _map.Mappings.Count; i++)
            {
                if (object.Equals(_map.Mappings[i], Mapping))
                    return this; // we have already added the Mapping to the mappings collection
            }

            _map.Mappings.Add(Mapping);
            return this;
        }
    }
}
