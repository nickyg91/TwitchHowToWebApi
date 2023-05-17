namespace Orchard.Models;

public class FruitModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CountryOfOrigin { get; set; }
    public string SkuNumber { get; set; }
    public int PiecesInInventory { get; set; }
    public bool IsArchived { get; set; }
}