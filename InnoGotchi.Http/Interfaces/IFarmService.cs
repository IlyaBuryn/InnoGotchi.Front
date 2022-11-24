using InnoGotchi.Components.DtoModels;
using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.Interfaces
{
    public interface IFarmService
    {
        public Task<ResponseModel<int?>> CreateFarm(FarmDto request);
        public Task<ResponseModel<bool?>> UpdateFarm(FarmDto request);
        public Task<ResponseModel<FarmDto>> GetFarmByUserId(int userId);
        public Task<ResponseModel<FarmDto>> GetFarmByFarmId(int farmId);
    }
}
