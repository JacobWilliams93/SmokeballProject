using SmokeballAPI.Enums;

namespace SmokeballAPI.Models;

public class SearchResultModel
{
   public string Keywords { get; set; }
   
   public string Url { get; set; }
   public SearchManagerEnum SearchType { get; set; }
   public IEnumerable<int> SearchPositions { get; set; }
}