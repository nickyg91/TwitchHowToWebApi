using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orchard.Models;
using Orchard.Services.Domain;

namespace Orchard.Web.Controllers;

[Route("api/fruit-inventory")]
[Produces("application/json")]
[Authorize]
[ApiController]
public class FruitInventoryController : ControllerBase
{
    private readonly IFruitInventoryManagementService _fruitInventoryManagementService;

    public FruitInventoryController(IFruitInventoryManagementService fruitInventoryManagementService)
    {
        _fruitInventoryManagementService = fruitInventoryManagementService;
    }

    [HttpPost("add")]
    public async Task<FruitModel> AddFruit(FruitModel model)
    {
        return await _fruitInventoryManagementService.AddFruit(model);
    }
        
    [HttpPut("fruit/{id:int}/update")]
    public async Task<bool> UpdateFruit(int id, FruitModel model)
    {
        return await _fruitInventoryManagementService.UpdateFruit(model);
    }
        
    [HttpDelete("fruit/{id:int}/archive")]
    public async Task<bool> ArchiveFruit(int id)
    {
        return await _fruitInventoryManagementService.RemoveFruit(id);
    }
        
    [HttpGet("fruit/unarchived")]
    public List<FruitModel> GetUnarchivedFruit()
    {
        return _fruitInventoryManagementService.GetUnarchivedFruit();
    }
    
    [HttpGet("seed"), AllowAnonymous]
    public async Task Seed()
    {
        var totalToMake = 1000;
        var tasks = new List<Task>();
        for (int i = 0; i < totalToMake; i++)
        {
            var fruit = new FruitModel
            {
                Name = $"Test{i}",
                SkuNumber = i.ToString(),
                CountryOfOrigin = "USA",
                PiecesInInventory = 100
            };
            await _fruitInventoryManagementService.AddFruit(fruit);
        }
    }
}