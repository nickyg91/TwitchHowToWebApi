using Orchard.Core.Enums;

namespace Orchard.Models;

public class FruitModel
{
    public int Id { get; set; }
    public int BasketId { get; set; }
    public int BasketFruitId { get; set; }
    public FruitType FruitType { get; set; }
    public Shape FruitShape { get; set; }
    public int Amount { get; set; }
}