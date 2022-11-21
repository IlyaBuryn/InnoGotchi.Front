using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.Interfaces
{
    public interface IFarmService
    {
        public Task<ResponseModel<int?>> CreateFarm(FarmModel request);
        public Task<ResponseModel<bool?>> UpdateFarm(FarmModel request);
        public Task<ResponseModel<FarmModel>> GetFarmByUserId(int userId);
        public Task<ResponseModel<FarmModel>> GetFarmByFarmId(int farmId);
    }
}
