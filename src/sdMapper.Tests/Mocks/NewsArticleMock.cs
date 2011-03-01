using System;
using sdMapper.Data;

namespace sdMapper.Tests.Mocks
{
    public class NewsArticleMock
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string SubTitle { get; set; }
        public string Body { get; set; }
        public string Author { get; set; }
    }

    public class NewsArticleMockMap : Map<NewsArticleMock>
    {
        public NewsArticleMockMap()
        {
            MapProperty(article => article.Title);
            MapProperty(article => article.Body).To("Text");
            MapProperty(article => article.SubTitle);
        }

        public override string TemplatePath
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
