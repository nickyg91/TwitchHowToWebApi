namespace Orchard.Models;

public class PaginatedFruitModel
{
    public int TotalFruit { get; set; }
    public int TotalPages { get; set; }
    public List<FruitModel> Fruit { get; set; } = new();
}