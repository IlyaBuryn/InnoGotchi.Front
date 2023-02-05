using InnoGotchi.BusinessLogic.BusinessModels;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Models.ResponseModels;
using Microsoft.AspNetCore.Http;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        public Task<ResponseModel<AuthenticateResponseDto>> SignIn(AuthRequestModel model);
        public Task<ResponseModel<AuthenticateResponseDto>> SignUp(AuthRequestModel model);
        public Task<ResponseModel<bool>> ChangePassword(PasswordUpdateModel model);
        public Task<ResponseModel<UserUpdateModel>> ChangeUserInfo(UserUpdateModel model, IFormFile image);
    }
}
