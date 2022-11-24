using InnoGotchi.Components.DtoModels;
using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.HttpServices
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpClientService<LoginModel> _httpServiceLoginHelper;
        private readonly IHttpClientService<RegisterModel> _httpServiceRegisterHelper;
        private readonly IHttpClientService<IdentityUserModel> _httpServiceUserHelper;

        public IdentityService(IHttpClientService<LoginModel> httpServiceLoginHelper,
            IHttpClientService<RegisterModel> httpServiceRegisterHelper,
            IHttpClientService<IdentityUserModel> httpServiceUserHelper)
        {
            _httpServiceLoginHelper = httpServiceLoginHelper;
            _httpServiceRegisterHelper = httpServiceRegisterHelper;
            _httpServiceUserHelper = httpServiceUserHelper;
        }

        public async Task<ResponseModel<AuthenticateResponseDto>> SignIn(LoginModel request)
        {
            return await _httpServiceLoginHelper.Post<AuthenticateResponseDto>(request, "account/login");
        }

        public async Task<ResponseModel<AuthenticateResponseDto>> SignUp(RegisterModel request)
        {
            return await _httpServiceRegisterHelper.Post<AuthenticateResponseDto>(request, "account/register");
        }

        public async Task<ResponseModel<bool?>> UpdateUserInfo(IdentityUserModel request)
        {
            return await _httpServiceUserHelper.Put<bool?>(request, "account/change-user-info");
        }

        public async Task<ResponseModel<bool?>> UpdatePassword(IdentityUserModel request)
        {
            return await _httpServiceUserHelper.Put<bool?>(request, "account/change-password");
        }
    }
}
