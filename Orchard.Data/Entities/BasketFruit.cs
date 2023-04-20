namespace Orchard.Data.Entities;

public class BasketFruit : BaseEntity
{
    public int FruitId { get; set; }
    public int BasketId { get; set; }
    public int Amount { get; set; }
    public Basket Basket { get; set; }
    public Fruit Fruit { get; set; }
}