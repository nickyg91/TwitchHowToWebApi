using Orchard.Models;

namespace Orchard.Services.Domain;

public interface IFruitInventoryManagementService
{
    Task<FruitModel> AddFruit(FruitModel fruit);
    Task<bool> RemoveFruit(int id);
    Task<bool> UpdateFruit(FruitModel fruit);
    List<FruitModel> GetUnarchivedFruit();
}