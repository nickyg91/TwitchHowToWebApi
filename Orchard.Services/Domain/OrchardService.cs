using AutoMapper;
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

    public async Task<FruitModel> AddFruit(FruitModel model)
    {
        var mappedDbFruit = _mapper.Map<FruitModel, Fruit>(model);

        await _fruitRepository.AddFruit(mappedDbFruit);
        return _mapper.Map<Fruit, FruitModel>(mappedDbFruit);
    }

    public async Task<bool> DeleteFruit(int fruitId)
    {
        return await _fruitRepository.RemoveFruit(fruitId);
    }

    public async Task<List<FruitModel>> GetAllFruit()
    {
        return _mapper
            .Map<List<Fruit>, List<FruitModel>>
                (await _fruitRepository.GetAllFruit());
    }

    public async Task<FruitModel?> GetFruitById(int fruitId)
    {
        var fruitFromDb = await _fruitRepository.GetFruitById(fruitId);
        if (fruitFromDb == null)
        {
            return null;
        }
        return _mapper.Map<Fruit, FruitModel>(fruitFromDb);
    }

    public async Task<BasketModel> CreateBasket(BasketModel model)
    {
        var dbMappedBasket = _mapper.Map<BasketModel, Basket>(model);
        dbMappedBasket = await _basketRepository.AddBasket(dbMappedBasket);
        return _mapper.Map<Basket, BasketModel>(dbMappedBasket);
    }

    public async Task<List<BasketModel>> GetAllBaskets(bool includeFruit)
    {
        var baskets = await _basketRepository.GetAllBaskets(includeFruit);
        var mappedBaskets = _mapper.Map<List<Basket>, List<BasketModel>>(baskets);
        return mappedBaskets;
    }

    public async Task<BasketModel?> GetBasketById(int id, bool includeFruit)
    {
        var basket = await _basketRepository.GetBasketById(id, includeFruit);

        if (basket == null)
        {
            return null;
        }
        
        var mappedBasket = _mapper.Map<Basket, BasketModel>(basket);
        return mappedBasket;
    }

    public async Task<bool> UpdateBasket(BasketModel basket)
    {
        var dbBasket = await _basketRepository.GetBasketById(basket.Id, true);
        if (dbBasket == null)
        {
            return false;
        }
        _mapper.Map(basket, dbBasket);
        return await _basketRepository.UpdateBasket(dbBasket);
    }

    public async Task<bool> DeleteBasket(int id)
    {
        return await _basketRepository.RemoveBasket(id);
    }
}