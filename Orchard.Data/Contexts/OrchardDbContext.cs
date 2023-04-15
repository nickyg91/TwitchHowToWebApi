using Microsoft.EntityFrameworkCore;
using Orchard.Data.Entities;
using Orchard.Data.EntityConfigurations;

namespace Orchard.Data.Contexts;

public class OrchardDbContext : DbContext
{
    public DbSet<Fruit> Fruit { get; set; }
    public DbSet<BasketFruit> BasketFruits { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    
    public OrchardDbContext(DbContextOptions<OrchardDbContext> options) : base(options)
    {
    }
   
    protected override void OnModelCreating(ModelBuilder builder) 
    {
        builder.HasDefaultSchema("orchard");
        var basketConfiguration = new BasketEntityBuilder();
        var fruitConfiguration = new FruitEntityBuilder();
        var basketFruitConfiguration = new BasketFruitEntityBuilder();
        basketConfiguration.Configure(builder.Entity<Basket>());
        fruitConfiguration.Configure(builder.Entity<Fruit>());
        basketFruitConfiguration.Configure(builder.Entity<BasketFruit>());
    }
}