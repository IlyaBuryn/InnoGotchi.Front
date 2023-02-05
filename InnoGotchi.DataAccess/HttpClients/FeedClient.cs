using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Interfaces.HttpClients;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.DataAccess.HttpClients
{
    public class FeedClient : IFeedClient
    {
        private readonly ICustomHttpClient<FeedDto> _httpServiceHelper;

        public FeedClient(ICustomHttpClient<FeedDto> httpServiceHelper)
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
            await _httpServiceHelper.Post<int?>(new FeedDto(), $"feed/recalculate/{farmId}");
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
