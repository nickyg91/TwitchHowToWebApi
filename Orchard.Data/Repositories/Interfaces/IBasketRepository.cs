using Orchard.Data.Entities;

namespace Orchard.Data.Repositories.Interfaces;

public interface IBasketRepository
{
    Task<Basket> AddBasket(Basket basket);
    Task<bool> RemoveBasket(int basketId);
    Task<bool> UpdateBasket(Basket basket);
    Task<List<Basket>> GetAllBaskets(bool includeFruit);
    Task<Basket?> GetBasketById(int id, bool includeFruit);

}