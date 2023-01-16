using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Interfaces.HttpClients;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.DataAccess.HttpClients
{
    public class CollabClient : ICollabClient
    {
        private readonly ICustomHttpClient<CollaboratorDto> _httpServiceHelper;

        public CollabClient(ICustomHttpClient<CollaboratorDto> httpServiceHelper)
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
