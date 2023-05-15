using Microsoft.AspNetCore.Mvc;
using Orchard.Data.Cache;
using Orchard.Models;

namespace Orchard.Web.Controllers;

[Route("api/cart")]
[Produces("application/json")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICacheService _cache;

    public CartController(ICacheService cache)
    {
        _cache = cache;
    }

    // need to figure out what this id will be for guests
    [HttpPut("update/{id}")]
    public async Task UpdateCart(string id, BasketModel basket)
    {
        await _cache.WriteObject(id, basket);
    }
        
    [HttpPut("{id}")]
    public async Task<BasketModel?> GetCart(string id)
    {
        return await _cache.ReadObject<BasketModel>(id);
    }
}