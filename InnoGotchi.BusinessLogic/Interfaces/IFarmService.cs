using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IFarmService
    {
        public Task<ResponseModel<FarmDto>> GetUserFarm(int userId);
        public Task<ResponseModel<FarmDto>> CheckFarmDetails(int farmId);
        public Task<ResponseModel<FarmDto>> CheckFarmReadOnlyDetails(int farmId);
        public Task<ResponseModel<int?>> CreateFarm(FarmDto farmDto);
        public Task<ResponseModel<CollaboratorDto>> InviteFriend(string username);
    }
}
