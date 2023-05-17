using AutoMapper;
using Orchard.Core.Enums;
using Orchard.Core.Exceptions;
using Orchard.Data.Entities;
using Orchard.Data.Repositories.Interfaces;
using Orchard.Models;

namespace Orchard.Services.Domain;

public class OrchardService : IOrchardService
{
    private readonly IFruitRepository _fruitRepository;
    private readonly IBasketRepository _basketRepository;
    private readonly IMapper _mapper;
    public OrchardService(
        IFruitRepository fruitRepository, 
        IBasketRepository basketRepository, 
        IMapper mapper)
    {
        _mapper = mapper;
        _fruitRepository = fruitRepository;
        _basketRepository = basketRepository;
    }

    public async Task<BasketModel> SubmitOrder(BasketModel model)
    {
        model.OrderStatus = OrderStatus.Submitted;
        var dbMappedBasket = _mapper.Map<BasketModel, Basket>(model);
        dbMappedBasket = await _basketRepository.SubmitBasketOrder(dbMappedBasket);
        return _mapper.Map<Basket, BasketModel>(dbMappedBasket);
    }

    public async Task<List<BasketModel>> GetAllBasketsByUserId(int userId)
    {
        var baskets = await _basketRepository.GetAllBasketsByUserId(userId);
        var mappedBaskets = _mapper.Map<List<Basket>, List<BasketModel>>(baskets);
        return mappedBaskets;
    }

    public async Task<BasketModel?> GetBasketById(int id, int userId)
    {
        var basket = await _basketRepository.GetBasketById(id);
        
        if (basket == null)
        {
            return null;
        }

        if (basket.UserId.HasValue && basket.UserId != userId)
        {
            throw new UnauthorizedBasketAccessException("You do not have permission to access this basket.");
        }
        
        var mappedBasket = _mapper.Map<Basket, BasketModel>(basket);
        return mappedBasket;
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