using InnoGotchi.Components.DtoModels;
using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.Interfaces
{
    public interface ICollabService
    {
        public Task<ResponseModel<int?>> CreateCollab(CollaboratorDto request);
        public Task<ResponseModel<bool?>> DeleteCollab(int userId);
        public Task<ResponseModel<List<CollaboratorDto>>> GetCollabsByFarm(int farmId);
        public Task<ResponseModel<List<CollaboratorDto>>> GetCollabsByUser(int userId);
        public Task<ResponseModel<CollaboratorDto>> GetUserData(string username);
    }
}
