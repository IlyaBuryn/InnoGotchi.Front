using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Interfaces.HttpClients;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.DataAccess.HttpClients
{
    public class FarmClient : IFarmClient
    {
        private readonly ICustomHttpClient<FarmDto> _httpServiceHelper;

        public FarmClient(ICustomHttpClient<FarmDto> httpServiceHelper)
        {
            _httpServiceHelper = httpServiceHelper;
        }

        public async Task<ResponseModel<int?>> CreateFarm(FarmDto request)
        {
            return await _httpServiceHelper.Post<int?>(request, "farm/create");
        }

        public async Task<ResponseModel<FarmDto>> GetFarmByUserId(int userId)
        {
            return await _httpServiceHelper.Get<FarmDto>($"farm/user/{userId}");
        }
        
        public async Task<ResponseModel<FarmDto>> GetFarmByFarmId(int farmId)
        {
            return await _httpServiceHelper.Get<FarmDto>($"farm/{farmId}");
        }

        public async Task<ResponseModel<bool?>> UpdateFarm(FarmDto request)
        {
            return await _httpServiceHelper.Put<bool?>(request, "farm/update");
        }
    }
}
