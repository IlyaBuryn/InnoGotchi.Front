using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.DataAccess.Interfaces.HttpClients
{
    public interface ICollabClient
    {
        public Task<ResponseModel<int?>> CreateCollab(CollaboratorDto request);
        public Task<ResponseModel<bool?>> DeleteCollab(int userId);
        public Task<ResponseModel<List<CollaboratorDto>>> GetCollabsByFarm(int farmId);
        public Task<ResponseModel<List<CollaboratorDto>>> GetCollabsByUser(int userId);
        public Task<ResponseModel<CollaboratorDto>> GetUserData(string username);
    }
}
