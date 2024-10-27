using SmokeballAPI.Enums;
using SmokeballAPI.Models;

namespace SmokeballAPI.Interfaces;

public interface ISearchManager
{
    public SearchManagerEnum SearchManagerType();
    public Task<SearchResultModel> GetSearchResultPositions(List<string> keywords, string url, int numResults);
}