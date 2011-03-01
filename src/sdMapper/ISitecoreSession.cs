using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sdMapper
{
    public interface ISitecoreSession : IDisposable
    {
        T Load<T>(Guid id) where T : class;
        T Load<T>(string path);
        IEnumerable<T> Load<T>(params Guid[] ids);
        IEnumerable<T> Load<T>(IEnumerable<Guid> ids);

        IEnumerable<T> Query<T>(string query);

        void Store(object entity);
        void SaveChanges();
    }
}
