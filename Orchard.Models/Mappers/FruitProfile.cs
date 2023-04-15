using AutoMapper;
using Orchard.Data.Entities;

namespace Orchard.Models.Mappers;

public class FruitProfile : Profile
{
   public FruitProfile()
   {
      CreateMap<FruitModel, Fruit>(MemberList.Destination)
         .ForMember(dst => dst.Id, x => x.MapFrom(src => src.Id))
         .ForMember(dst => dst.FruitShape, x => x.MapFrom(src => src.FruitShape))
         .ForMember(dst => dst.FruitType, x => x.MapFrom(src => src.FruitType))
         .ReverseMap();
   }
}