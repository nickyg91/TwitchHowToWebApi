using Orchard.Models;

namespace Orchard.Services.Domain;

public interface IOrchardService
{
    Task<List<BasketModel>> GetAllBasketsByUserId(int userId);
    Task<BasketModel?> GetBasketById(int id, int userId);
}