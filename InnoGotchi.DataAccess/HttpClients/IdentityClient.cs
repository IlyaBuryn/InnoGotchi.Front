using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Interfaces.HttpClients;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.DataAccess.HttpClients
{
    public class IdentityClient : IIdentityClient
    {
        private readonly ICustomHttpClient<AuthenticateRequestDto> _httpLoginClient;
        private readonly ICustomHttpClient<IdentityUserDto> _httpUserClient;

        public IdentityClient(ICustomHttpClient<AuthenticateRequestDto> httpLoginClient,
            ICustomHttpClient<IdentityUserDto> httpUserClient)
        {
            _httpLoginClient = httpLoginClient;
            _httpUserClient = httpUserClient;
        }

        public async Task<ResponseModel<AuthenticateResponseDto>> SignIn(AuthenticateRequestDto request)
        {
            return await _httpLoginClient.Post<AuthenticateResponseDto>(request, "account/login");
        }

        public async Task<ResponseModel<AuthenticateResponseDto>> SignUp(IdentityUserDto request)
        {
            return await _httpUserClient.Post<AuthenticateResponseDto>(request, "account/register");
        }

        public async Task<ResponseModel<bool?>> UpdateUserInfo(IdentityUserDto request)
        {
            return await _httpUserClient.Put<bool?>(request, "account/change-user-info");
        }

        public async Task<ResponseModel<bool?>> UpdatePassword(IdentityUserDto request)
        {
            return await _httpUserClient.Put<bool?>(request, "account/change-password");
        }
    }
}
