using AutoMapper;
using Orchard.Core.Exceptions;
using Orchard.Data.Entities;
using Orchard.Data.Repositories.Interfaces;
using Orchard.Models;

namespace Orchard.Services.Domain;

public class FruitInventoryManagementService : IFruitInventoryManagementService
{
    private readonly IMapper _mapper;
    private readonly IFruitRepository _fruitRepository;

    public FruitInventoryManagementService(IMapper mapper, IFruitRepository fruitRepository)
    {
        _mapper = mapper;
        _fruitRepository = fruitRepository;
    }

    public async Task<FruitModel> AddFruit(FruitModel fruit)
    {
        var hasExistingSkuNumber = await _fruitRepository.DoesSkuNumberExist(fruit.SkuNumber);
        if (hasExistingSkuNumber)
        {
            throw new DuplicateSkuNumberException($"An item with SKU Number {fruit.SkuNumber} already exists.");
        }

        var fruitToAdd = _mapper.Map<Fruit>(fruit);
        var addedFruit = await _fruitRepository.AddFruit(fruitToAdd);
        _mapper.Map(addedFruit, fruit);
        return fruit;
    }

    public async Task<bool> RemoveFruit(int id)
    {
        return await _fruitRepository.ArchiveFruit(id);
    }

    public async Task<bool> UpdateFruit(FruitModel fruit)
    {
        var fruitInDb = await _fruitRepository.GetFruitById(fruit.Id);

        if (fruitInDb == null)
        {
            throw new FruitNotFoundException($"{fruit.Name} not found!");
        }
        
        _mapper.Map(fruitInDb, fruit);

        return await _fruitRepository.UpdateFruit(fruitInDb);
    }

    public List<FruitModel> GetUnarchivedFruit()
    {
        var unarchivedFruit = _fruitRepository.GetAllFruit().Where(x => !x.IsArchived);
        
        return _mapper.Map<List<FruitModel>>(unarchivedFruit);
    }
}