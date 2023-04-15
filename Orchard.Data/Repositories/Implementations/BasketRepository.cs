using Microsoft.EntityFrameworkCore;
using Orchard.Data.Contexts;
using Orchard.Data.Entities;
using Orchard.Data.Repositories.Interfaces;

namespace Orchard.Data.Repositories.Implementations;

public class BasketRepository : IBasketRepository
{
    private readonly OrchardDbContext _ctx;

    public BasketRepository(OrchardDbContext ctx)
    {
        _ctx = ctx;
    }
    
    public async Task<Basket> AddBasket(Basket basket)
    {
        _ctx.Baskets.Add(basket);
        await _ctx.SaveChangesAsync();
        return basket;
    }

    public async Task<bool> RemoveBasket(int basketId)
    {
        var basketToDelete = await _ctx.Baskets.SingleOrDefaultAsync(x => x.Id == basketId);
        if (basketToDelete == null)
        {
            return false;
        }
        
        _ctx.Baskets.Remove(basketToDelete);
        return await _ctx.SaveChangesAsync() > 0;
    }
}