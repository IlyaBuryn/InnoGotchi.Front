using AutoMapper;
using InnoGotchi.BusinessLogic.BusinessModels;
using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.MapProfiles
{
    public class UserUpdateMapProfile : Profile
    {
        public UserUpdateMapProfile()
        {
            CreateMap<UserUpdateModel, IdentityUserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));
        }
    }
}
