using InnoGotchi.Components.DtoModels;
using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.HttpServices
{
    public class FeedService : IFeedService
    {
        private readonly IHttpClientService<FeedDto> _httpServiceHelper;

        public FeedService(IHttpClientService<FeedDto> httpServiceHelper)
        {
            _httpServiceHelper = httpServiceHelper;
        }

        public async Task<ResponseModel<int?>> DrinkPet(FeedDto request)
        {
            return await _httpServiceHelper.Post<int?>(request, "feed/water");
        }

        public async Task<ResponseModel<int?>> FeedPet(FeedDto request)
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
