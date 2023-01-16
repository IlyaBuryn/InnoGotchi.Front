using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.DataAccess.Interfaces.HttpClients
{
    public interface IFarmClient
    {
        public Task<ResponseModel<int?>> CreateFarm(FarmDto request);
        public Task<ResponseModel<bool?>> UpdateFarm(FarmDto request);
        public Task<ResponseModel<FarmDto>> GetFarmByUserId(int userId);
        public Task<ResponseModel<FarmDto>> GetFarmByFarmId(int farmId);
    }
}
