using System;

namespace sdMapper
{
    public class SitecoreSession : ISitecoreSession
    {
        public SitecoreSession()
        {

        }

        #region ISitecoreSession Members

        public T Load<T>(Guid id)
        {
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

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            
        }

        #endregion
    }
}
