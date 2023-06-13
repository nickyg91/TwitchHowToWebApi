using Grpc.Core;
using Orchard.Models;
using Orchard.Services.Domain;

namespace Orchard.Grpc.Services;

public class OrdersService : Orders.OrdersBase
{
    private readonly IOrchardService _orchardService;

    public OrdersService(IOrchardService orchardService)
    {
        _orchardService = orchardService;
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
        var submittedOrder = await _orchardService.SubmitOrder(basket, request.UserId);
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
}