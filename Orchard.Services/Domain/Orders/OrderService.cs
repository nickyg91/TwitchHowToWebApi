using AutoMapper;
using Orchard.Core.Enums;
using Orchard.Core.Exceptions;
using Orchard.Data.Entities;
using Orchard.Data.Repositories.Interfaces;
using Orchard.Models;

namespace Orchard.Services.Domain.Orders;

public class OrderService : IOrderService
{
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public OrderService(IBasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }
    
    public async Task<BasketModel> SubmitOrder(BasketModel model, int? userId)
    {
        model.OrderStatus = OrderStatus.Submitted;
        var dbMappedBasket = _mapper.Map<BasketModel, Basket>(model);
        dbMappedBasket.UserId = userId;
        dbMappedBasket = await _basketRepository.SubmitBasketOrder(dbMappedBasket);
        return _mapper.Map<Basket, BasketModel>(dbMappedBasket);
    }

    public async Task<bool> CancelOrder(int id, int userId)
    {
        var dbBasket = await _basketRepository.GetBasketById(id);

        if (dbBasket == null)
        {
            return false;
        }
        
        if (!CanBeCanceled(dbBasket.OrderStatus))
        {
            throw new InvalidOrderStatusException(
                $"Unable to cancel order that has a status of {dbBasket.OrderStatus}.");
        }
        return await _basketRepository.CancelBasketOrder(dbBasket);
    }

    public async Task<List<BasketModel>> GetOrdersForUser(int userId)
    {
        var orders = await _basketRepository.GetAllBasketsByUserId(userId);
        return _mapper.Map<List<BasketModel>>(orders);
    }

    public async Task<List<BasketFruitModel>> GetOrderDetails(int orderId, int userId)
    {
        var details = await _basketRepository.GetBasketById(orderId);

        if (details == null)
        {
            throw new BasketNotFoundException("Unable to find this basket!");
        }
        
        if (details.UserId != userId)
        {
            throw new UnauthorizedBasketAccessException("User cannot access this order!");
        }

        var fruit = details.Fruit;
        return _mapper.Map<List<BasketFruitModel>>(fruit);
    }

    private bool CanBeCanceled(OrderStatus orderStatus)
    {
        var cancellableStatuses = new []
        {
            OrderStatus.Completed,
            OrderStatus.Cancelled,
            OrderStatus.Returned,
            OrderStatus.ReturnRequested
        };

        return !cancellableStatuses.Contains(orderStatus);
    }
}