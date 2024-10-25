using System.Text;
using System.Text.RegularExpressions;
using SmokeballAPI.Interfaces;

namespace SmokeballAPI.Managers;

public class GoogleSearchManager : ISearchManager
{
    private readonly string _baseUrl = "https://google.com/search";
    private readonly HttpClient _httpClient;

    public GoogleSearchManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<int>> GetSearchResultPositions(List<string> keywords, string url, int numResults)
    {
        string requestUrl = $"{_baseUrl}?q={String.Join('+', keywords)}&num={numResults}";
        HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
        var bytes = await response.Content.ReadAsByteArrayAsync();
        var htmlResponse = Encoding.UTF8.GetString(bytes);
        return ParseSearchResults(htmlResponse, url);
    }

    private List<int> ParseSearchResults(string searchResults, string url)
    {
        List<int> result = new List<int>();
        string pattern = @"<a[^>]*href=""([^""]*)[^>]*><div";
        MatchCollection regexResult = Regex.Matches(searchResults, pattern);
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