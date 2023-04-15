using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Orchard.Data.Entities;

namespace Orchard.Models.Mappers;

public class BasketFruitProfile : Profile
{
    public BasketFruitProfile()
    {
        CreateMap<FruitModel, BasketFruit>()
            .ForMember(dst => dst.Id, x => x.MapFrom(src => src.BasketFruitId))
            .ForMember(dst => dst.Amount, x => x.MapFrom(src => src.Amount))
            .ForMember(dst => dst.FruitId, x => x.MapFrom(src => src.Id))
            .ForMember(dst => dst.BasketId, x => x.MapFrom(src => src.BasketId))
            .ForMember(dst => dst.Fruit, x => x.MapFrom(src => src))
            .EqualityComparison((fm, bf) => fm.BasketFruitId == bf.Id);

        CreateMap<BasketFruit, FruitModel>()
            .ForMember(dst => dst.Id, x => x.MapFrom(src => src.Fruit.Id))
            .ForMember(dst => dst.Amount, x => x.MapFrom(src => src.Amount))
            .ForMember(dst => dst.BasketId, x => x.MapFrom(src => src.BasketId))
            .ForMember(dst => dst.FruitType, x => x.MapFrom(src => src.Fruit.FruitType))
            .ForMember(dst => dst.FruitShape, x => x.MapFrom(src => src.Fruit.FruitShape))
            .ForMember(dst => dst.BasketFruitId, x => x.MapFrom(src => src.Id))
            .EqualityComparison((bf, f) => bf.Id == f.BasketFruitId);
    }
}