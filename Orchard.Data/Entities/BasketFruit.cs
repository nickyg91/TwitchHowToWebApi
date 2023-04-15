using Orchard.Data.Entities;

namespace Orchard.Data.Entities;

public class BasketFruit
{
    public int Id { get; set; }
    public int FruitId { get; set; }
    public int BasketId { get; set; }
    public int Amount { get; set; }
    public Basket Basket { get; set; }
    public Fruit Fruit { get; set; }
}