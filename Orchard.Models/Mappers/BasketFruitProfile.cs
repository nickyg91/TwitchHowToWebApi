using AutoMapper;
using Orchard.Data.Entities;

namespace Orchard.Models.Mappers;

public class BasketFruitProfile : Profile
{
    public BasketFruitProfile()
    {
        CreateMap<BasketFruit, BasketFruitModel>(MemberList.Destination);

        CreateMap<BasketFruitModel, BasketFruit>(MemberList.Destination);
    }
}