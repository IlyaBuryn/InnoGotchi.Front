using AutoMapper;
using InnoGotchi.BusinessLogic.BusinessModels;
using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.MapProfiles
{
    public class PasswordUpdateMapProfile : Profile
    {
        public PasswordUpdateMapProfile()
        {
            CreateMap<PasswordUpdateModel, IdentityUserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.NewPassword));
        }
    }
}
