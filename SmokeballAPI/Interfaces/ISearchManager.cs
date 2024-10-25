namespace SmokeballAPI.Interfaces;

public interface ISearchManager
{
    Task<IEnumerable<int>> GetSearchResultPositions(List<string> keywords, string url, int numResults);
}