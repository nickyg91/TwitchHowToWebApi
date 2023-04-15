using Orchard.Core.Enums;

namespace Orchard.Data.Entities;

public class Fruit
{
    public int Id { get; set; }
    public FruitType FruitType { get; set; }
    public Shape FruitShape { get; set; }
    // public ICollection<BasketFruit> BasketFruits { get; set; }
}