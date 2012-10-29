Google Search Result Parser
---------------

This application sends a request to Google search engine pretending to be a browser and parses the returned HTML for search results. The key points of the application are

**Sending a request using WebClient**

    public static string GetSearchResultHtlm(string keywords)
    {
    	StringBuilder sb = new StringBuilder("http://www.google.com/search?q=");
    	sb.Append(keywords);
    	return webClient.DownloadString(sb.ToString());
    }

**Parsing the HTML returned by WebClient**

    public static List<String> ParseSearchResultHtml(string html)
    {
        Regex extractUrl = new Regex(@"[&?](?:q|url)=([^&]+)", RegexOptions.Compiled);
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

**See also my blog entries on the subject: [Part I] and [Part II]**

  [Part I]: http://justmycode.blogspot.com.au/2012/09/playing-with-google-search-results.html
  [Part II]: http://justmycode.blogspot.com.au/2012/09/playing-with-google-search-results-2.html
