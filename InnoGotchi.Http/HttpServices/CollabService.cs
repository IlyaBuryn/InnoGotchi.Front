using InnoGotchi.Http.Components;
using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace InnoGotchi.Http.HttpServices
{
    public class CollabService : ICollabService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly HttpServiceHelper<CollaboratorModel> _httpServiceHelper;

        public CollabService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            _httpServiceHelper = new HttpServiceHelper<CollaboratorModel>(_configuration, _httpClient, _httpContextAccessor);
        }

        public async Task<ResponseModel<int?>> CreateCollab(CollaboratorModel request)
        {
            return await _httpServiceHelper.Post<int?>(request, "collab/create");
        }

        public async Task<ResponseModel<bool?>> DeleteCollab(int userId)
        {
            return await _httpServiceHelper.Delete<bool?>($"collab/delete/{userId}");
        }

        public async Task<ResponseModel<List<CollaboratorModel>>> GetCollabsByFarm(int farmId)
        {
            return await _httpServiceHelper.Get<List<CollaboratorModel>>($"collab/farm/{farmId}");
        }

        public async Task<ResponseModel<List<CollaboratorModel>>> GetCollabsByUser(int userId)
        {
            return await _httpServiceHelper.Get<List<CollaboratorModel>>($"collab/user/{userId}");
        }

        public async Task<ResponseModel<AuthResponseModel>> GetUserData(string username)
        {
            return await _httpServiceHelper.Get<AuthResponseModel>($"account/{username}");
        }
    }
}
