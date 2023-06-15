using Orchard.Core.Enums;
using Orchard.Models;

namespace Orchard.Web.ApplicationServices;

public interface IOrdersApplicationService
{
    Task<BasketModel> SubmitOrder(BasketModel model, int userId);
    Task<bool> CancelOrder(int orderId, int userId);
    Task<OrderStatus> CheckOrderStatus(int orderId, int userId);
    Task<List<BasketModel>> GetOrdersForUser(int userId);
    Task<List<BasketFruitModel>> GetMoreOrderDetails(int orderId, int userId);
}