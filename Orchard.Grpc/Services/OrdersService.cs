using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Orchard.Models;
using Orchard.Services.Domain.Orders;

namespace Orchard.Grpc.Services;

public class OrdersService : Orders.OrdersBase
{
    private readonly IOrderService _orderService;

    public OrdersService(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public override async Task<OrderResponse> SubmitOrder(OrderRequest request, ServerCallContext context)
    {
        var basket = new BasketModel
        {
            OrderStatus = Core.Enums.OrderStatus.Submitted,
            Fruit = request.Fruit.Select(x => new BasketFruitModel
            {
                FruitId = x.FruitId,
                Amount = x.Amount
            }).ToList()
        };
        var submittedOrder = await _orderService.SubmitOrder(basket, request.UserId);
        return new OrderResponse
        {
            OrderStatus = (OrderStatus)submittedOrder.OrderStatus,
            Id = submittedOrder.Id
        };
    }

    public override async Task<CheckOrderResponse> CheckOrderStatus(CheckOrderRequest request, ServerCallContext context)
    {
        return await base.CheckOrderStatus(request, context);
    }

    public override async Task<CancelOrderResponse> CancelOrder(CancelOrderRequest request, ServerCallContext context)
    {
        var isCancelled = await _orderService.CancelOrder(request.OrderId, request.UserId);

        return new CancelOrderResponse
        {
            IsCancelled = isCancelled
        };
    }

    public override async Task<UserOrdersResponse> GetOrdersForUser(UserOrdersRequest request, ServerCallContext context)
    {
        var orders = await _orderService.GetOrdersForUser(request.UserId);

        return new UserOrdersResponse
        {
            Orders =
            {
                orders.Select(x => new UserOrdersResponse.Types.Order
                {
                    Id = x.Id,
                    Status = (OrderStatus)x.OrderStatus,
                    AmountOfFruit = x.Fruit.Count,
                    CreatedAtUtc = x.CreatedAtUtc.ToTimestamp()
                })
            }
        };
    }

    public override async Task<MoreOrderDetailsResponse> GetMoreOrderInfo(MoreOrderDetailsRequest request, ServerCallContext context)
    {
        var details = await _orderService.GetOrderDetails(request.Id, request.UserId);
        return new MoreOrderDetailsResponse
        {
            Fruit =
            {
                details.Select(x => new BasketFruit
                {
                    Amount = x.Amount,
                    FruitId = x.FruitId,
                    BasketId = x.BasketId,
                    Name = x.FruitName
                })
            }
        };
    }
}