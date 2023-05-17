using AutoMapper;
using Orchard.Data.Entities;
using Orchard.Data.Repositories.Interfaces;
using Orchard.Models;

namespace Orchard.Services.Domain;

public class OrchardFruitService : IOrchardFruitService
{
    private readonly IMapper _mapper;
    private readonly IFruitRepository _fruitRepository;

    public OrchardFruitService(IMapper mapper, IFruitRepository fruitRepository)
    {
        _mapper = mapper;
        _fruitRepository = fruitRepository;
    }

    public List<FruitModel> GetAllFruit()
    {
        var fruit = _fruitRepository.GetAllFruit().ToList();
        if (fruit.Any())
        {
            return _mapper.Map<List<FruitModel>>(fruit);
        }
        return new List<FruitModel>();
    }

    public List<FruitModel> SearchFruit(string searchQuery)
    {
        var searchResults = _fruitRepository.SearchFruitByNameOrCountryOfOrigin(searchQuery).ToList();
        if (searchResults.Any())
        {
            return _mapper.Map<List<FruitModel>>(searchResults);
        }

        return new List<FruitModel>();
    }

    public List<FruitModel> GetPaginatedFruit(int pageNumber, int pageSize)
    {
        var paginatedFruit = _fruitRepository.GetAllFruitPaginated(pageNumber, pageSize);
        if (paginatedFruit.Any())
        {
            return _mapper.Map<List<FruitModel>>(paginatedFruit);
        }

        return new List<FruitModel>();
    }

    public async Task<FruitModel?> GetFruitById(int id)
    {
        var fruit = await _fruitRepository.GetFruitById(id);
        if (fruit != null)
        {
            return _mapper.Map<FruitModel>(fruit);
        }
        return null;
    }
}