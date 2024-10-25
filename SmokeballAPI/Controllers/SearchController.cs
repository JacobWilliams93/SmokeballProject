using Microsoft.AspNetCore.Mvc;
using SmokeballAPI.Interfaces;

namespace SmokeballAPI.Controllers;

[ApiController]
public class SearchController: ControllerBase
{
    private readonly ISearchManager _searchManager;

    public SearchController(ISearchManager searchManager)
    {
        _searchManager = searchManager;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery]string searchString, [FromQuery] string targetUrl)
    {
       var result = await _searchManager.GetSearchResultPositions(searchString.Split(' ').ToList(), targetUrl, 100);
       return Ok(result);
    } 
}