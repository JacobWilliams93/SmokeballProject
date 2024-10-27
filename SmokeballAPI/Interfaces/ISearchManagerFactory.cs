using SmokeballAPI.Enums;

namespace SmokeballAPI.Interfaces;

public interface ISearchManagerFactory
{
    ISearchManager GetSearchManager(SearchManagerEnum searchManagerEnum);
}