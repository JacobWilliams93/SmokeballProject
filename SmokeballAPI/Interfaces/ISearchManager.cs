using SmokeballAPI.Enums;

namespace SmokeballAPI.Interfaces;

public interface ISearchManager
{
    public SearchManagerEnum SearchManagerType();
    public Task<IEnumerable<int>> GetSearchResultPositions(List<string> keywords, string url, int numResults);
}