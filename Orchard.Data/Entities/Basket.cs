namespace Orchard.Data.Entities;

public class Basket : BaseEntity
{
    public string Name { get; set; }
    public ICollection<BasketFruit> Fruit { get; set; }
}