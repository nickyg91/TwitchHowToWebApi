using Orchard.Grpc;
using Orchard.Models;
using OrderStatus = Orchard.Core.Enums.OrderStatus;

namespace Orchard.Web.ApplicationServices;

public class OrdersApplicationService : IOrdersApplicationService
{
    private readonly Orders.OrdersClient _ordersClient;

    public OrdersApplicationService(Orders.OrdersClient ordersClient)
    {
        _ordersClient = ordersClient;
    }
    public async Task<BasketModel> SubmitOrder(BasketModel model, int userId)
    {
        var orderRequest = new OrderRequest
        {
            Fruit =
            {
                model.Fruit.Select(x => new BasketFruit
                {
                  Amount = x.Amount,
                  FruitId = x.FruitId
                })
            },
            UserId = userId
        };

        var orderResponse = await _ordersClient.SubmitOrderAsync(orderRequest);

        return new BasketModel
        {
            Id = orderResponse.Id,
            OrderStatus = (OrderStatus)orderResponse.OrderStatus
        };
    }

    public async Task<bool> CancelOrder(int orderId, int userId)
    {
        var response = await _ordersClient.CancelOrderAsync(new CancelOrderRequest
        {
            UserId = userId,
            OrderId = orderId
        });
        return response.IsCancelled;
    }

    public async Task<OrderStatus> CheckOrderStatus(int orderId, int userId)
    {
        var status = await _ordersClient.CheckOrderStatusAsync(new CheckOrderRequest
        {
            Id = orderId,
            UserId = userId
        });
        return (OrderStatus)status.OrderStatus;
    }

    public async Task<List<BasketModel>> GetOrdersForUser(int userId)
    {
        var response = await _ordersClient.GetOrdersForUserAsync(new UserOrdersRequest
        {
            UserId = userId
        });

        var orders = response.Orders.Select(x => new BasketModel
        {
            Id = x.Id,
            OrderStatus = (OrderStatus)x.Status,
            TotalFruit = x.AmountOfFruit,
            CreatedAtUtc = x.CreatedAtUtc.ToDateTime() 
        }).ToList();
        return orders;
    }

    public async Task<List<BasketFruitModel>> GetMoreOrderDetails(int orderId, int userId)
    {
        var details = await _ordersClient.GetMoreOrderInfoAsync(new MoreOrderDetailsRequest
        {
            Id = orderId,
            UserId = userId
        });

        return details.Fruit.Select(x => new BasketFruitModel
        {
            Amount = x.Amount,
            FruitId = x.FruitId,
            BasketId = x.BasketId,
            FruitName = x.Name
        }).ToList();
    }
}