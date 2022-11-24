using InnoGotchi.Components.DtoModels;
using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.HttpServices
{
    public class CollabService : ICollabService
    {
        private readonly IHttpClientService<CollaboratorDto> _httpServiceHelper;

        public CollabService(IHttpClientService<CollaboratorDto> httpServiceHelper)
        {
            _httpServiceHelper = httpServiceHelper;
        }

        public async Task<ResponseModel<int?>> CreateCollab(CollaboratorDto request)
        {
            return await _httpServiceHelper.Post<int?>(request, "collab/create");
        }

        public async Task<ResponseModel<bool?>> DeleteCollab(int userId)
        {
            return await _httpServiceHelper.Delete<bool?>($"collab/delete/{userId}");
        }

        public async Task<ResponseModel<List<CollaboratorDto>>> GetCollabsByFarm(int farmId)
        {
            return await _httpServiceHelper.Get<List<CollaboratorDto>>($"collab/farm/{farmId}");
        }

        public async Task<ResponseModel<List<CollaboratorDto>>> GetCollabsByUser(int userId)
        {
            return await _httpServiceHelper.Get<List<CollaboratorDto>>($"collab/user/{userId}");
        }

        public async Task<ResponseModel<CollaboratorDto>> GetUserData(string username)
        {
            return await _httpServiceHelper.Get<CollaboratorDto>($"account/{username}");
        }
    }
}
