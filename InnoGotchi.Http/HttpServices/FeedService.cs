using InnoGotchi.Http.Components;
using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace InnoGotchi.Http.HttpServices
{
    public class FeedService : IFeedService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly HttpServiceHelper<FeedModel> _httpServiceHelper;

        public FeedService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            _httpServiceHelper = new HttpServiceHelper<FeedModel>(_configuration, _httpClient, _httpContextAccessor);
        }

        public async Task<ResponseModel<int?>> DrinkPet(FeedModel request)
        {
            return await _httpServiceHelper.Post<int?>(request, "feed/water");
        }

        public async Task<ResponseModel<int?>> FeedPet(FeedModel request)
        {
            return await _httpServiceHelper.Post<int?>(request, "feed/food");
        }

        public async Task RecalculatePetsNeeds(int farmId)
        {
            await _httpServiceHelper.Post<int?>(null, $"feed/recalculate/{farmId}");
        }

        public async Task<ResponseModel<double?>> FeedingFoodPeriod(int farmId)
        {
            return await _httpServiceHelper.Get<double?>($"feed/food/{farmId}");
        }

        public async Task<ResponseModel<double?>> FeedingDrinkPeriod(int farmId)
        {
            return await _httpServiceHelper.Get<double?>($"feed/water/{farmId}");
        }
    }
}
