using AutoMapper;
using Orchard.Core.Services;
using Orchard.Models.Authentication;

namespace Orchard.Models.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        PasswordHashService passwordHashService = new PasswordHashService();
        CreateMap<UserModel, Data.Entities.User>()
            .ForMember(dst => dst.HashedPassword,
                x => x.MapFrom(src => passwordHashService.HashPassword(src.Password)));
        CreateMap<UserModel, Data.Entities.User>().ReverseMap();
        
        CreateMap<LoginModel, UserModel>()
            .ForMember(dst => dst.Password, x => x.MapFrom(src => src.Password))
            .ForMember(dst => dst.Email, x => x.MapFrom(src => src.Email));
    }
}