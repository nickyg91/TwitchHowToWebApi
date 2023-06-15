using AutoMapper;
using Orchard.Data.Entities;

namespace Orchard.Models.Mappers;

public class BasketFruitProfile : Profile
{
    public BasketFruitProfile()
    {
        CreateMap<BasketFruit, BasketFruitModel>(MemberList.Destination)
            .ForMember(dst => dst.FruitName, x => x.MapFrom(src => src.Fruit.Name));

        CreateMap<BasketFruitModel, BasketFruit>(MemberList.Destination);
    }
}