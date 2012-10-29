using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace Search
{
    public static class Helper
    {
        public static WebClient webClient = new WebClient();
        public static Regex extractUrl = new Regex(@"[&?](?:q|url)=([^&]+)", RegexOptions.Compiled);

        public static string GetSearchResultHtlm(string keywords)
        {
            StringBuilder sb = new StringBuilder("http://www.google.com/search?q=");
            sb.Append(keywords);
            return webClient.DownloadString(sb.ToString());
        }

        public static List<String> ParseSearchResultHtml(string html)
        {
            List<String> searchResults = new List<string>();

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            var nodes = (from node in doc.DocumentNode.SelectNodes("//a")
                         let href = node.Attributes["href"]
                         where null != href
                         where href.Value.Contains("/url?") || href.Value.Contains("?url=")
                         select href.Value).ToList();

            foreach (var node in nodes)
            {
                var match = extractUrl.Match(node);
                string test = HttpUtility.UrlDecode(match.Groups[1].Value);
                searchResults.Add(test);
            }

            return searchResults;
        }
    }
}
