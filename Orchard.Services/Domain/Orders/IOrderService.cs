using Orchard.Models;

namespace Orchard.Services.Domain.Orders;

public interface IOrderService
{
    
    Task<BasketModel> SubmitOrder(BasketModel model, int? userId);
    Task<bool> CancelOrder(int id, int userId);
    Task<List<BasketModel>> GetOrdersForUser(int userId);
    Task<List<BasketFruitModel>> GetOrderDetails(int orderId, int userId);
    
}