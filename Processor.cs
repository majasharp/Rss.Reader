using Rss.Reader.Models;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Rss.Reader
{
    public class Processor
    {
        public List<string> ParseConfig(string filePath)
        {
            var urls = new List<string>();
            string line = null;

            using (StreamReader sr = new StreamReader(filePath))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    urls.Add(line);
                }
            }
            return urls;
        }
        public List<Feed> InstantiateFeeds(List<string> urls)
        {
            var feeds = new List<Feed>();
            for (int i = 0; i < urls.Count; i++)
            {
                using var reader = XmlReader.Create(urls[i]);
                var loadedFeed = SyndicationFeed.Load(reader);

                var feed = new Feed()
                {
                    Id = i,
                    Url = urls[i],
                    Name = loadedFeed.Title.Text,
                    Articles = new List<Article>()
                };

                var articleId = 1;

                foreach (var article in loadedFeed.Items)
                {
                    if (articleId == 25)
                    {
                        break;
                    }

                    feed.Articles.Add(new Article()
                    {
                        Id = articleId,
                        Url = article.Links[0].Uri.ToString(),
                        Title = article.Title.Text,
                        Author = article.Authors.FirstOrDefault()?.Name ?? "",
                        Created = article.PublishDate.DateTime,
                        Content = ""
                    });

                    articleId++;
                }

                feeds.Add(feed);
            }

            return feeds;
        }
    }
}
