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

    public async Task<bool> UpdateBasket(Basket basket)
    {
        _ctx.Baskets.Update(basket);
        return await _ctx.SaveChangesAsync() > 0;
    }

    public async Task<List<Basket>> GetAllBaskets(bool includeFruit)
    {
        if (!includeFruit)
        {
            return await _ctx
                .Baskets
                .ToListAsync();
        }

        return await _ctx.Baskets
            .Include(x => x.Fruit)
            .ThenInclude(x => x.Fruit)
            .ToListAsync();
    }

    public async Task<Basket?> GetBasketById(int id, bool includeFruit)
    {
        if (!includeFruit)
        {
            return await _ctx
                .Baskets
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        return await _ctx.Baskets
            .Include(x => x.Fruit)
            .ThenInclude(x => x.Fruit)
            .SingleOrDefaultAsync(x => x.Id == id);
    }
}