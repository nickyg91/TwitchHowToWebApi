using Microsoft.EntityFrameworkCore;
using Orchard.Data.Contexts;
using Orchard.Data.Entities;
using Orchard.Data.Repositories.Interfaces;

namespace Orchard.Data.Repositories.Implementations;

public class FruitRepository : IFruitRepository
{
    private readonly OrchardDbContext _ctx;

    public FruitRepository(OrchardDbContext ctx)
    {
        _ctx = ctx;
    }
    
    public async Task<Fruit> AddFruit(Fruit fruit)
    {
        _ctx.Fruit.Add(fruit);
        await _ctx.SaveChangesAsync();
        return fruit;
    }

    public async Task<bool> RemoveFruit(int fruitId)
    {
        var fruitToDelete = await _ctx.Fruit.SingleOrDefaultAsync(x => x.Id == fruitId);
        if (fruitToDelete == null)
        {
            return false;
        }
        
        _ctx.Fruit.Remove(fruitToDelete);
        return await _ctx.SaveChangesAsync() > 0;
    }

    public async Task<List<Fruit>> GetAllFruit()
    {
        return await _ctx.Fruit.AsNoTracking().ToListAsync();
    }

    public async Task<Fruit?> GetFruitById(int fruitId)
    {
        return await _ctx.Fruit.SingleOrDefaultAsync(x => x.Id == fruitId);
    }
}