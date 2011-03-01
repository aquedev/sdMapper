using System;
using sdMapper.Utilities;
using sdMapper.Data;

namespace sdMapper
{
    public class SitecoreSession : ISitecoreSession
    {
        private readonly ISitecoreDataService _dataService;

        public SitecoreSession(ISitecoreDataService dataService)
        {
            _dataService = dataService;
        }

        public T Load<T>(Guid id) where T : class
        {
            Guard.NotEmptyGuid(id, "id");
            ThinItem item = _dataService.GetItem(id);
            if (item == null)
                return (T)null;
            else
                throw new NotImplementedException();
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


        #region IDisposable Members

        public void Dispose()
        {
            
        }

        #endregion
    }
}
