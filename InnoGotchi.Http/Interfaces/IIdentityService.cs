using InnoGotchi.Components.DtoModels;
using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.Interfaces
{
    public interface IIdentityService
    {
        public Task<ResponseModel<AuthenticateResponseDto>> SignIn(LoginModel request);
        public Task<ResponseModel<AuthenticateResponseDto>> SignUp(RegisterModel request);
        public Task<ResponseModel<bool?>> UpdateUserInfo(IdentityUserModel request);
        public Task<ResponseModel<bool?>> UpdatePassword(IdentityUserModel request);
    }
}
