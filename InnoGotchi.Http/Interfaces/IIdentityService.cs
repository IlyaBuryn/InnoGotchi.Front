using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.Interfaces
{
    public interface IIdentityService
    {
        public Task<ResponseModel<AuthResponseModel>> SignIn(LoginModel request);
        public Task<ResponseModel<AuthResponseModel>> SignUp(RegisterModel request);
        public Task<ResponseModel<bool?>> UpdateUserInfo(IdentityUserModel request);
        public Task<ResponseModel<bool?>> UpdatePassword(IdentityUserModel request);
    }
}
