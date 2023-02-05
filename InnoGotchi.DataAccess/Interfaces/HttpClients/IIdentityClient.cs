using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.DataAccess.Interfaces.HttpClients
{
    public interface IIdentityClient
    {
        public Task<ResponseModel<AuthenticateResponseDto>> SignIn(AuthenticateRequestDto request);
        public Task<ResponseModel<AuthenticateResponseDto>> SignUp(IdentityUserDto request);
        public Task<ResponseModel<bool?>> UpdateUserInfo(IdentityUserDto request);
        public Task<ResponseModel<bool?>> UpdatePassword(IdentityUserDto request);
    }
}
