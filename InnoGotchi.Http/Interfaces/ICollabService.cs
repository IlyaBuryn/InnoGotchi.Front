using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.Interfaces
{
    public interface ICollabService
    {
        public Task<ResponseModel<int?>> CreateCollab(CollaboratorModel request);
        public Task<ResponseModel<bool?>> DeleteCollab(int userId);
        public Task<ResponseModel<List<CollaboratorModel>>> GetCollabsByFarm(int farmId);
        public Task<ResponseModel<List<CollaboratorModel>>> GetCollabsByUser(int userId);
        public Task<ResponseModel<AuthResponseModel>> GetUserData(string username);
    }
}
