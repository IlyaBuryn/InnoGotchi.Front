using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;
using Microsoft.Extensions.Configuration;
using InnoGotchi.Http.Components;
using Microsoft.AspNetCore.Http;

namespace InnoGotchi.Http.HttpServices
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private HttpServiceHelper<LoginModel> _httpServiceLoginHelper;
        private HttpServiceHelper<RegisterModel> _httpServiceRegisterHelper;
        private HttpServiceHelper<IdentityUserModel> _httpServiceUserHelper;
        public IdentityService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            _httpServiceLoginHelper = new HttpServiceHelper<LoginModel>(_configuration, _httpClient, _httpContextAccessor);
            _httpServiceRegisterHelper = new HttpServiceHelper<RegisterModel>(_configuration, _httpClient, _httpContextAccessor);
            _httpServiceUserHelper = new HttpServiceHelper<IdentityUserModel>(_configuration, _httpClient, _httpContextAccessor);
        }

        public async Task<ResponseModel<AuthResponseModel>> SignIn(LoginModel request)
        {
            return await _httpServiceLoginHelper.Post<AuthResponseModel>(request, "account/login");
        }

        public async Task<ResponseModel<AuthResponseModel>> SignUp(RegisterModel request)
        {
            return await _httpServiceRegisterHelper.Post<AuthResponseModel>(request, "account/register");
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
