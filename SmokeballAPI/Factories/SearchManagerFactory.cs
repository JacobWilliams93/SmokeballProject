using SmokeballAPI.Enums;
using SmokeballAPI.Interfaces;

namespace SmokeballAPI.Factories;

public class SearchManagerFactory
{
    private readonly IEnumerable<ISearchManager> _searchManagers;

    public SearchManagerFactory(IEnumerable<ISearchManager> searchManagers)
    {
        _searchManagers = searchManagers;
    }

    public ISearchManager GetSearchManager(SearchManagerEnum searchManagerEnum)
    {
        var manager =  _searchManagers.FirstOrDefault(searchManager => searchManager.SearchManagerType() == searchManagerEnum);
        if (manager == null)
        {
            throw new ArgumentException("There is no Search Manager with the given type");
        }
        return manager;
    }
}