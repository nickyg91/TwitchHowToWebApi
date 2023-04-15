using AutoMapper;
using Orchard.Data.Entities;

namespace Orchard.Models.Mappers;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<BasketModel, Basket>()
            .ForMember(dst => dst.Id, x => x.MapFrom(src => src.Id))
            .ForMember(dst => dst.Name, x => x.MapFrom(src => src.Name))
            .ForMember(dst => dst.Fruit, x => x.MapFrom(src => src.Fruit))
            .ReverseMap();
    }
}