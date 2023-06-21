using Orchard.Core.Enums;

namespace Orchard.Models;

public class BasketModel
{
    public int Id { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public List<BasketFruitModel> Fruit { get; set; }
    public int TotalFruit { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}