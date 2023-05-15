using AutoMapper;
using Orchard.Data.Entities;

namespace Orchard.Models.Mappers;

public class FruitProfile : Profile
{
   public FruitProfile()
   {
      CreateMap<FruitModel, Fruit>(MemberList.Destination)
         .ReverseMap();
   }
}