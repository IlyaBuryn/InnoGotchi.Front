using AutoMapper;
using InnoGotchi.BusinessLogic.BusinessModels;
using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.MapProfiles
{
    public class SignInMapProfile : Profile
    {
        public SignInMapProfile()
        {
            CreateMap<AuthRequestModel, AuthenticateRequestDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));

            CreateMap<PasswordUpdateModel, AuthenticateRequestDto>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.OldPassword))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username));
        }
    }
}
