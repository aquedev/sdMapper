using System;
using Sitecore.Data;
using Sitecore.Configuration;
using sdMapper.Data;

namespace sdMapper.SitecoreImpl
{
    public class SitecoreDataService : ISitecoreDataService
    {
        Database _db;

        public SitecoreDataService(string databaseName)
        {
            _db = GetDatabase(databaseName);
        }

        public SitecoreDataService()
        {
            _db = Databases.CurrentDatabase;
        }

        private Database GetDatabase(string databaseName)
        {
            return Factory.GetDatabase(databaseName);
        }

        public ThinItem GetItem(Guid id)
        {
            return ThinItem.FromSitecoreItem(_db.GetItem(new ID(id)));
        }

        public ThinItem GetItem(string path)
        {
            return ThinItem.FromSitecoreItem(_db.GetItem(path));
        }

    }
}
