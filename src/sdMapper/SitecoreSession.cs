using System;
using sdMapper.Utilities;
using sdMapper.Data;

namespace sdMapper
{
    public class SitecoreSession : ISitecoreSession
    {
        private readonly ISitecoreDataService _dataService;
        private readonly IMapFinder _mapFinder;
        private readonly ItemConverter _converter;
        
        public SitecoreSession(ISitecoreDataService dataService,
            IMapFinder mapFinder,
            ItemConverter converter)
        {
            _converter = converter;
            _mapFinder = mapFinder;
            _dataService = dataService;
        }

        public T Load<T>(Guid id) where T : class
        {
            Guard.NotEmptyGuid(id, "id");
            ThinItem item = _dataService.GetItem(id);
            if (item == null)
                return (T)null;
            else
            {
                IMap map = _mapFinder.FindMap<T>();
                if (map == null)
                    throw new InvalidOperationException(String.Format("Cannot load entity because there is no map associated with type ({0})", typeof(T)));

                return (T)_converter.Convert(item, map);
            }
        }

        public T Load<T>(string path)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<T> Load<T>(params Guid[] ids)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<T> Load<T>(System.Collections.Generic.IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<T> Query<T>(string query)
        {
            throw new NotImplementedException();
        }

        public void Store(object entity)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }


        private object CreateEntity(Type type)
        {
            return Activator.CreateInstance(type);
        }

        #region IDisposable Members

        public void Dispose()
        {
            
        }

        #endregion
    }
}
