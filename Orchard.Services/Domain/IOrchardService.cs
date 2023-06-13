using Orchard.Models;

namespace Orchard.Services.Domain;

public interface IOrchardService
{
    Task<BasketModel> SubmitOrder(BasketModel model, int? userId);
    Task<List<BasketModel>> GetAllBasketsByUserId(int userId);
    Task<BasketModel?> GetBasketById(int id, int userId);
    Task<bool> CancelOrder(int id, int userId);
}