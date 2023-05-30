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

    public async Task<bool> ArchiveFruit(int fruitId)
    {
        var fruitToArchive = await _ctx.Fruit.SingleOrDefaultAsync(x => x.Id == fruitId);
        if (fruitToArchive == null)
        {
            return false;
        }

        fruitToArchive.IsArchived = true;
        return await _ctx.SaveChangesAsync() > 0;
    }

    public IEnumerable<Fruit> GetAllFruit()
    {
        return _ctx.Fruit.AsNoTracking();
    }

    public IEnumerable<Fruit> GetAllFruitPaginated(int pageNumber, int perPage)
    {
        var fruitQuery = _ctx.Fruit.AsNoTracking().Skip((pageNumber - 1) * perPage).Take(perPage);
        return fruitQuery;
    }

    public async Task<Fruit?> GetFruitById(int fruitId)
    {
        return await _ctx.Fruit
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == fruitId);
    }

    //very basic, better ways to do this for sure.
    //full text search, elastisearch, etc.
    public IEnumerable<Fruit> SearchFruitByNameOrCountryOfOrigin(string nameOrCountryOfOrigin)
    {
        var fruitQuery = _ctx.Fruit.Where(x =>
            x.CountryOfOrigin.Contains(nameOrCountryOfOrigin) || x.Name.Contains(nameOrCountryOfOrigin));
        return fruitQuery;
    }

    public async Task<bool> UpdateFruit(Fruit fruit)
    {
        _ctx.Fruit.Update(fruit);
        return await _ctx.SaveChangesAsync() > 0;
    }

    public async Task<bool> DoesSkuNumberExist(string skuNumber)
    {
        return await _ctx.Fruit.Select(x => x.SkuNumber).AnyAsync(x => x == skuNumber);
    }
}