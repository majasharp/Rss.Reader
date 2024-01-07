using Rss.Reader.Models;

namespace Rss.Reader
{
    public class ConsoleDisplay
    {
        public void ShowFeeds(List<Feed> feeds)
        {
            while (true)
            {
                string header = string.Format("{0, -20}{1,-100}\n", "Id", "Feed Name");

                Console.WriteLine(header);

                string output;

                foreach (var feed in feeds)
                {
                    output = string.Format("{0, -20}{1,-100}", feed.Id, feed.Name);
                    Console.WriteLine(output);
                }

                var feedId = Console.ReadLine();
                if (!string.IsNullOrEmpty(feedId))
                {
                    Console.Clear();
                    var currentFeed = feeds.FirstOrDefault(x => x.Id == int.Parse(feedId));
                    ShowArticles(currentFeed);
                }
            }
        }

        private void ShowArticles(Feed currentFeed)
        {
            while (true)
            {
                string header = string.Format("{0, -5}{1, -80}{2, -5}\n", "Id", "Title", "Published");

                Console.WriteLine(header);

                string output;

                foreach (var article in currentFeed.Articles)
                {
                    var formattedTitle = article.Title.Length > 78 ? article.Title.Substring(0, 78) : article.Title;
                    output = string.Format("{0, -5}{1, -80}{2, -5}", article.Id, formattedTitle, article.Created.ToString("dd MMM"));
                    Console.WriteLine(output);
                }

                var articleId = Console.ReadLine();
                if (!string.IsNullOrEmpty(articleId))
                {
                    if (articleId == "b")
                    {
                        Console.Clear();
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        var currentArticle = currentFeed.Articles.First(x => x.Id == int.Parse(articleId));
                        ShowArticle(currentArticle);
                    }
                }
            }
        }

        private void ShowArticle(Article article)
        {
            Console.WriteLine("Title:" + article.Title);
            Console.WriteLine("Author:" + article.Author);
            Console.WriteLine("Published:" + article.Created);
            Console.WriteLine("Link:" + article.Url + "\n");
            Console.WriteLine(article.Content);

            if (Console.ReadLine() == "b")
            {
                Console.Clear();
                return;
            }
        }
    }
}
