using Orchard.Core.Enums;

namespace Orchard.Data.Entities;

public class Basket : BaseEntity
{
    public int? UserId { get; set; }
    public User? User { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public ICollection<BasketFruit> Fruit { get; set; }
}