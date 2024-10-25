namespace SmokeballAPI.Interfaces;

public interface ISearchManager
{
    public Task<IEnumerable<int>> SearchUrlPlacement(string keywords, string targetUrl);
}