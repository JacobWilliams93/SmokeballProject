using SmokeballAPI.Enums;

namespace SmokeballAPI.Managers;

public class GoogleSearchManager(IHttpClientFactory httpClientFactory) : SearchManagerBase(httpClientFactory)
{
    private readonly string _baseUrl = "https://www.google.com/search";

    public override SearchManagerEnum SearchManagerType() => SearchManagerEnum.GoogleSearchManager;

    protected override string GetRequestUrl(IEnumerable<string> keywords, int numResults) => $"{_baseUrl}?q={String.Join('+', keywords)}&num={numResults}&udm=14";
    protected override string GetRegexPattern() => @"<a[^>]*href=""\/([^""]*)[^>]*><div";

}