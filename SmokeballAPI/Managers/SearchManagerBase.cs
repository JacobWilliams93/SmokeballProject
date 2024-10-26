using System.Text.RegularExpressions;
using SmokeballAPI.Enums;
using SmokeballAPI.Interfaces;

namespace SmokeballAPI.Managers;

public abstract class SearchManagerBase : ISearchManager
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public SearchManagerBase(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    protected abstract string GetRequestUrl(IEnumerable<string> keywords, int numResults);
    protected abstract string GetRegexPattern();
    public abstract SearchManagerEnum SearchManagerType();

    public async Task<IEnumerable<int>> GetSearchResultPositions(List<string> keywords, string url, int numResults)
    {
        // udm=14 makes google only return web results
        var client = _httpClientFactory.CreateClient("Client");
        string requestUrl = GetRequestUrl(keywords, numResults);
        HttpResponseMessage response = await client.GetAsync(requestUrl);
        var htmlResponse = await response.Content.ReadAsStringAsync();
        return ParseSearchResults(htmlResponse, url);
    }

    private List<int> ParseSearchResults(string searchResults, string url)
    {
        List<int> result = new List<int>();
        MatchCollection regexResult = Regex.Matches(searchResults, GetRegexPattern());
        for(int i=0; i < regexResult.Count; i++)
        {
            if (regexResult[i].Groups[1].Value.Contains(url))
            {
                result.Add(i + 1);
            }
        }
        if(result.Count == 0)
        {
            result.Add(0);
        }
        return result;
    }
}