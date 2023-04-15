using Orchard.Models;

namespace Orchard.Services.Domain;

public interface IOrchardService
{
    Task<FruitModel> AddFruit(FruitModel model);
    Task<bool> DeleteFruit(int fruitId);
    Task<List<FruitModel>> GetAllFruit();
    Task<FruitModel?> GetFruitById(int fruitId);
    Task<BasketModel> CreateBasket(BasketModel model);
    Task<List<BasketModel>> GetAllBaskets(bool includeFruit);
    Task<BasketModel?> GetBasketById(int id, bool includeFruit);
    Task<bool> UpdateBasket(BasketModel basket);
    Task<bool> DeleteBasket(int id);
}