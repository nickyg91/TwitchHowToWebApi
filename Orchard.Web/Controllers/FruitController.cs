using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orchard.Models;
using Orchard.Services.Domain;

namespace Orchard.Web.Controllers;
[Route("api/fruit")]
[ApiController]
[Authorize]
[Produces("application/json")]
public class FruitController : ControllerBase
{
    private readonly IOrchardFruitService _orchardFruitService;
    
    public FruitController(IOrchardFruitService orchardFruitService)
    {
        _orchardFruitService = orchardFruitService;
    }

    [HttpGet("page/{page}/size/{size}")]
    public List<FruitModel> GetAllFruits(int page, int size)
    {
        return _orchardFruitService.GetPaginatedFruit(page, size);
    }
    
    [HttpGet("{id:int}")]
    public async Task<FruitModel?> GetFruitById(int id)
    {
        return await _orchardFruitService.GetFruitById(id);
    }
    
    [HttpGet]
    public List<FruitModel> GetAllFruit()
    {
        return _orchardFruitService.GetAllFruit();
    }
    
    [HttpGet("search/{query}")]
    public List<FruitModel> DeleteFruit(string query)
    {
        return _orchardFruitService.SearchFruit(query);
    }
}