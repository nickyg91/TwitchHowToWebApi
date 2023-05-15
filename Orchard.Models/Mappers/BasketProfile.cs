using AutoMapper;
using Orchard.Data.Entities;

namespace Orchard.Models.Mappers;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<BasketModel, Basket>()
            .ReverseMap();
    }
}