using Microsoft.EntityFrameworkCore;
using Orchard.Core.Enums;
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
    
    public async Task<Basket> SubmitBasketOrder(Basket basket)
    {
        _ctx.Baskets.Add(basket);
        await _ctx.SaveChangesAsync();
        return basket;
    }

    public async Task<bool> CancelBasketOrder(Basket basket)
    {
        basket.OrderStatus = OrderStatus.Cancelled;
        return await _ctx.SaveChangesAsync() > 0;
    }
    
    public async Task<List<Basket>> GetAllBasketsByUserId(int userId)
    {
        return await _ctx.Baskets
            .Include(x => x.Fruit)
            .ToListAsync();
    }

    public async Task<Basket?> GetBasketById(int id)
    {
        return await _ctx.Baskets
            .Include(x => x.Fruit)
            .ThenInclude(x => x.Fruit)
            .SingleOrDefaultAsync(x => x.Id == id);
    }
}