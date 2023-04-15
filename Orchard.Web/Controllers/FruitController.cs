using Microsoft.AspNetCore.Mvc;
using Orchard.Models;
using Orchard.Services.Domain;

namespace Orchard.Web.Controllers;
[Route("api/fruit")]
[ApiController]
public class FruitController : ControllerBase
{
    private readonly IOrchardService _orchardService;
    
    public FruitController(IOrchardService orchardService)
    {
        _orchardService = orchardService;
    }

    [HttpGet()]
    public async Task<List<FruitModel>> GetAllFruits()
    {
        return await _orchardService.GetAllFruit();
    }

    [HttpGet("{id:int}")]
    public async Task<FruitModel?> GetFruitById(int id)
    {
        return await _orchardService.GetFruitById(id);
    }
    
    [HttpPost("create")]
    public async Task<FruitModel> CreateFruit(FruitModel fruit)
    {
        return await _orchardService.AddFruit(fruit);
    }
    
    [HttpDelete("delete/{id:int}")]
    public async Task<bool> DeleteFruit(int id)
    {
        return await _orchardService.DeleteFruit(id);
    }
}