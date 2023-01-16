using AutoMapper;
using InnoGotchi.BusinessLogic.BusinessModels;
using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.MapProfiles
{
    public class SignUpMapProfile : Profile
    {
        public SignUpMapProfile()
        {
            CreateMap<AuthRequestModel, IdentityUserDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.IdentityRoleId, opt => opt.MapFrom(src => src.IdentityRoleId.GetValueOrDefault()));
        }
    }
}
