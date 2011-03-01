using System;
using Sitecore;
using Sitecore.Data;
using Sitecore.Configuration;
using Sitecore.Data.Items;

namespace sdMapper.SitecoreImpl
{
    public static class Databases
    {
        public static Database Core
        {
            get { return Factory.GetDatabase(DatabaseNames.Core); }
        }
        public static Database Master
        {
            get { return Factory.GetDatabase(DatabaseNames.Master); }
        }

        public static Database Web
        {
            get { return Factory.GetDatabase(DatabaseNames.Web); }
        }

        public static Database CurrentDatabase
        {
            get
            {
                // this is used by code that executes under the sitecore editor UI, such as item event handlers
                if (Context.Database.Name == DatabaseNames.Core)
                    return Context.ContentDatabase;

                return Context.Database;
            }
        }

        public static void SetContextDatabase(string name)
        {
            SetContextDatabase(Factory.GetDatabase(name));
        }

        public static void SetContextDatabase(Item item)
        {
            if (item != null)
                SetContextDatabase(item.Database);
        }

        public static void SetContextDatabase(Database db)
        {
            Context.Database = db;
        }
    }

    public static class DatabaseNames
    {
        public const string Core = "core";
        public const string Master = "master";
        public const string Web = "web";
    }
}
