using Orchard.Models;

namespace Orchard.Services.Domain;

public interface IOrchardService
{
    Task<FruitModel> AddFruit(FruitModel model);
    Task<bool> DeleteFruit(int fruitId);
    Task<List<FruitModel>> GetAllFruit();
    Task<FruitModel?> GetFruitById(int fruitId);
}