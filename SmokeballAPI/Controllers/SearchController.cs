﻿using Microsoft.AspNetCore.Mvc;
using SmokeballAPI.Enums;
using SmokeballAPI.Factories;
using SmokeballAPI.Interfaces;

namespace SmokeballAPI.Controllers;

[ApiController]
public class SearchController: ControllerBase
{
    private readonly ISearchManagerFactory _searchManagerFactory;

    public SearchController(ISearchManagerFactory searchManagerFactory)
    {
        _searchManagerFactory = searchManagerFactory;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string searchString, [FromQuery] string targetUrl, [FromQuery]SearchManagerEnum engineType)
    {
        var manager = _searchManagerFactory.GetSearchManager(engineType);
        var result = await manager.GetSearchResultPositions(searchString.Split(' ').ToList(), targetUrl, 100);
        return Ok(result);
    } 
}