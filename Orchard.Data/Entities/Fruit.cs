using Orchard.Core.Enums;

namespace Orchard.Data.Entities;

public class Fruit : BaseEntity
{
    public FruitType FruitType { get; set; }
    public Shape FruitShape { get; set; }
}