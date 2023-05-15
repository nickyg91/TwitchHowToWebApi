namespace Orchard.Models;

public class BasketModel
{
    public int Id { get; set; }
    public List<BasketFruitModel> Fruit { get; set; }
}