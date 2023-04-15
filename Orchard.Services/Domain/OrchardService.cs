using AutoMapper;
using Orchard.Data.Entities;
using Orchard.Data.Repositories.Interfaces;
using Orchard.Models;

namespace Orchard.Services.Domain;

public class OrchardService : IOrchardService
{
    private readonly IFruitRepository _fruitRepository;
    private readonly IMapper _mapper;
    public OrchardService(IFruitRepository fruitRepository, IMapper mapper)
    {
        _mapper = mapper;
        _fruitRepository = fruitRepository;
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
}