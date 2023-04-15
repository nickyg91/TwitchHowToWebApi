using Orchard.Data.EntityConfigurations;

namespace Orchard.Data.Entities;

public class Basket
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<BasketFruit> Fruit { get; set; }
}