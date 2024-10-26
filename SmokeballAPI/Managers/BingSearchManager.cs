using SmokeballAPI.Enums;
using SmokeballAPI.Interfaces;

namespace SmokeballAPI.Managers;

public class BingSearchManager : SearchManagerBase
{
    private readonly string _baseUrl= "https://www.bing.com/search";
    
    public BingSearchManager(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    protected override string GetRequestUrl(IEnumerable<string> keywords, int numResults) => $"{_baseUrl}?q={String.Join('+', keywords)}&count={numResults}";

    protected override string GetRegexPattern() => @"<cite>(.*?)<\/cite>";

    public override SearchManagerEnum SearchManagerType() => SearchManagerEnum.BingSearchManager;
}