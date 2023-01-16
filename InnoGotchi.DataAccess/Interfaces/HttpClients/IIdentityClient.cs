using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Models.IdentityModels;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.DataAccess.Interfaces.HttpClients
{
    public interface IIdentityClient
    {
        public Task<ResponseModel<AuthenticateResponseDto>> SignIn(LoginModel request);
        public Task<ResponseModel<AuthenticateResponseDto>> SignUp(RegisterModel request);
        public Task<ResponseModel<bool?>> UpdateUserInfo(IdentityUserModel request);
        public Task<ResponseModel<bool?>> UpdatePassword(IdentityUserModel request);
    }
}
