using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Orchard.Data.Entities;

namespace Orchard.Models.Mappers;

public class BasketFruitProfile : Profile
{
    public BasketFruitProfile()
    {
        CreateMap<BasketFruit, BasketFruitModel>(MemberList.Destination)
            .ForMember(dst => dst.FruitId, x => x.MapFrom(src => src.Id));
    }
}