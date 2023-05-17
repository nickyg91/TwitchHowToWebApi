using Orchard.Models;

namespace Orchard.Services.Domain;

public interface IOrchardFruitService
{
    List<FruitModel> GetAllFruit();
    List<FruitModel> SearchFruit(string searchQuery);
    List<FruitModel> GetPaginatedFruit(int pageNumber, int pageSize);
    Task<FruitModel?> GetFruitById(int id);
}