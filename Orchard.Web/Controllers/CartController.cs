using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orchard.Core.Exceptions;
using Orchard.Data.Cache;
using Orchard.Models.Authentication;

namespace Orchard.Web.Controllers;

[Route("api/cart")]
[Produces("application/json")]
[ApiController]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICacheService _cache;
    private readonly IAuthenticatedUser _authenticatedUser;

    public CartController(ICacheService cache, IAuthenticatedUser user)
    {
        _cache = cache;
        _authenticatedUser = user;
    }

    // need to figure out what this id will be for guests
    [HttpPut("update")]
    public async Task UpdateCart(Dictionary<int, int> basket)
    {
        string cartId = $"cart:{_authenticatedUser.Id}";
        await _cache.WriteObjectToHash(cartId, basket);
    }
        
    [HttpGet]
    public async Task<Dictionary<string, string>?> GetCart()
    {
        string cartId = $"cart:{_authenticatedUser.Id}";
        try
        {
            return await _cache.ReadObjectFromHash(cartId);
        }
        catch (CachedItemNotFoundException e)
        {
            await _cache.WriteObjectToHash(cartId, new Dictionary<string, string>());
            return await _cache.ReadObjectFromHash(cartId);
        }
    }
}