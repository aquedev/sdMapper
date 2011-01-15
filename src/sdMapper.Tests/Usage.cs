using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;


namespace sdMapper.Tests
{
    public class Usage
    {
        public class NewsArticle
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
        }

        public void Can_get_entity_from_session()
        {
            var itemStore = new ItemStore();
            itemStore.Initialise();

            using (var session = itemStore.OpenSession())
            {
                var newsArticleByPath = session.Load<NewsArticle>("/sitecore/content/Home/News/Article");
                //or
                var newsArticleById = session.Load<NewsArticle>("{3F2504E0-4F89-11D3-9A0C-0305E82C3301}");
            }
        }

        public void Can_update_entity()
        {
            var itemStore = new ItemStore();
            itemStore.Initialise();

            using (var session = itemStore.OpenSession())
            {
                var newsArticleByPath = session.Load<NewsArticle>("/sitecore/content/Home/News/Article");
                newsArticleByPath.Title = "New title";
                session.SaveChanges();
            }
        }

        public void Can_create_and_save_entities()
        {
            var itemStore = new ItemStore();
            itemStore.Initialise();

            using (var session = itemStore.OpenSession())
            {
                var newsFolder = session.Load<Folder>("/sitecore/content/Home/News");
                var newsArticle = new NewsArticle { Title = "New article" };
                session.Store(newsArticle, newsFolder);
                //or
                session.Store(newsArticle, "/sitecore/content/Home/News");
                //or
                session.Store(newsArticle, "{3F2504E0-4F89-11D3-9A0C-0305E82C3301}");

                session.SaveChanges();
                
            }

        }

    }
}
