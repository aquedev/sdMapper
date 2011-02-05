using System;
using System.Collections.Generic;

namespace sdMapper.Data
{
    public interface IItemProvider
    {
        ThinItem GetItem(Guid id);
        ThinItem GetItem(string path);
        IEnumerable<ThinItem> GetItems(IEnumerable<Guid> ids);
        IEnumerable<ThinItem> GetItemsByQuery(string query);
    }
}
