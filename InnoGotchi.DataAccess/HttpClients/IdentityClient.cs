using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Interfaces.HttpClients;
using InnoGotchi.DataAccess.Models.IdentityModels;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.DataAccess.HttpClients
{
    public class IdentityClient : IIdentityClient
    {
        private readonly ICustomHttpClient<LoginModel> _httpServiceLoginHelper;
        private readonly ICustomHttpClient<RegisterModel> _httpServiceRegisterHelper;
        private readonly ICustomHttpClient<IdentityUserModel> _httpServiceUserHelper;

        public IdentityClient(ICustomHttpClient<LoginModel> httpServiceLoginHelper,
            ICustomHttpClient<RegisterModel> httpServiceRegisterHelper,
            ICustomHttpClient<IdentityUserModel> httpServiceUserHelper)
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
