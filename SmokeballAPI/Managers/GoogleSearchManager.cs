using SmokeballAPI.Interfaces;

namespace SmokeballAPI.Managers;

public class GoogleSearchManager : ISearchManager
{
    public async Task<IEnumerable<int>> SearchUrlPlacement(string keywords, string targetUrl)
    {
        return new List<int>(){1,2,3};
    }
}