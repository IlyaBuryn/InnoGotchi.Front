using InnoGotchi.Http.Components;
using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace InnoGotchi.Http.HttpServices
{
    public class FarmService : IFarmService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly HttpServiceHelper<FarmModel> _httpServiceHelper;

        public FarmService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            _httpServiceHelper = new HttpServiceHelper<FarmModel>(_configuration, _httpClient, _httpContextAccessor);
        }

        public async Task<ResponseModel<int?>> CreateFarm(FarmModel request)
        {
            return await _httpServiceHelper.Post<int?>(request, "farm/create");
        }

        public async Task<ResponseModel<FarmModel>> GetFarmByUserId(int userId)
        {
            return await _httpServiceHelper.Get<FarmModel>($"farm/user/{userId}");
        }
        
        public async Task<ResponseModel<FarmModel>> GetFarmByFarmId(int farmId)
        {
            return await _httpServiceHelper.Get<FarmModel>($"farm/{farmId}");
        }

        public async Task<ResponseModel<bool?>> UpdateFarm(FarmModel request)
        {
            return await _httpServiceHelper.Put<bool?>(request, "farm/update");
        }
    }
}
