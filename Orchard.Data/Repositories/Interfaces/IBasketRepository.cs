using Orchard.Data.Entities;

namespace Orchard.Data.Repositories.Interfaces;

public interface IBasketRepository
{
    Task<Basket> AddBasket(Basket basket);
    Task<bool> RemoveBasket(int basketId);
}