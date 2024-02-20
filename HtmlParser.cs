using System.Net;
using HtmlAgilityPack;
using System.Text;

namespace Rss.Reader
{
    public class HtmlParser
    {
        public async Task<string> ParseHtmlAsync(string url)
        {
            try
            {
                string html = await GetHtmlAsync(url);

                StringBuilder sb = new StringBuilder();

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                var paragraphNodes = doc.DocumentNode.SelectNodes("//p");
                if (paragraphNodes != null && paragraphNodes.Any())
                {
                    foreach (var paragraphNode in paragraphNodes)
                    {
                        sb.AppendLine(paragraphNode.InnerText + "\n");
                    }
                }
                else
                {
                    var textNodes = doc.DocumentNode.DescendantsAndSelf()
                        .Where(n => n.NodeType == HtmlNodeType.Text && !string.IsNullOrWhiteSpace(n.InnerText))
                        .OrderByDescending(n => n.InnerText.Length)
                        .Take(5);

                    foreach (var textNode in textNodes)
                    {
                        sb.AppendLine(textNode.InnerText.Trim() + "\n");
                    }
                }

                return WebUtility.HtmlDecode(sb.ToString());
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        private async Task<string> GetHtmlAsync(string url)
        {
            using HttpClient client = new HttpClient();
            return await client.GetStringAsync(url);
        }
    }
}
