namespace Orchard.Models;

public class BasketFruitModel
{
    public int Id { get; set; }
    public int BasketId { get; set; }
    public int FruitId { get; set; }
    public string FruitName { get; set; }
    public int Amount { get; set; }
}