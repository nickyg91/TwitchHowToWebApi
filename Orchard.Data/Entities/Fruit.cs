namespace Orchard.Data.Entities;

public class Fruit : BaseEntity
{
    public string Name { get; set; }
    public string SkuNumber { get; set; }
    public string CountryOfOrigin { get; set; }
    public int PiecesInInventory { get; set; }
}