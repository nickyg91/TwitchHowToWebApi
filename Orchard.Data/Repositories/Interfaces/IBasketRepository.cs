using Orchard.Data.Entities;

namespace Orchard.Data.Repositories.Interfaces;

public interface IBasketRepository
{
    Task<Basket> SubmitBasketOrder(Basket basket);
    Task<bool> CancelBasketOrder(Basket basket);
    Task<List<Basket>> GetAllBasketsByUserId(int userId);
    Task<Basket?> GetBasketById(int id);

}