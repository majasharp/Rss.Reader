namespace Rss.Reader.Models
{
    public class Feed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Article> Articles { get; set; }
    }
}
