using Orchard.Data.Entities;

namespace Orchard.Data.Repositories.Interfaces;

public interface IFruitRepository
{
    Task<Fruit> AddFruit(Fruit fruit);
    Task<bool> RemoveFruit(int fruitId);
    Task<List<Fruit>> GetAllFruit();
    Task<Fruit?> GetFruitById(int fruitId);
}