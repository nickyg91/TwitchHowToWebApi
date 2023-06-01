using Orchard.Data.Entities;

namespace Orchard.Data.Repositories.Interfaces;

public interface IFruitRepository
{
    Task<Fruit> AddFruit(Fruit fruit);
    Task<bool> ArchiveFruit(int fruitId);
    IEnumerable<Fruit> GetAllFruit();
    IEnumerable<Fruit> GetAllFruitPaginated(int pageNumber, int perPage);
    Task<Fruit?> GetFruitById(int fruitId);
    IEnumerable<Fruit> SearchFruitByNameOrCountryOfOrigin(string nameOrCountryOfOrigin);
    Task<bool> UpdateFruit(Fruit fruit);
    Task<bool> DoesSkuNumberExist(string skuNumber);
    Task<(int totalFruit, int totalPages)> GetTotalPagesAndTotalFruit(int pageSize);
}