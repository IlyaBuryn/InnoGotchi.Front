using InnoGotchi.Components.DtoModels;
using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.Interfaces
{
    public interface IFeedService
    {
        public Task<ResponseModel<int?>> FeedPet(FeedDto request);
        public Task<ResponseModel<int?>> DrinkPet(FeedDto request);
        public Task RecalculatePetsNeeds(int farmId);
        public Task<ResponseModel<double?>> FeedingFoodPeriod(int farmId);
        public Task<ResponseModel<double?>> FeedingDrinkPeriod(int farmId);
    }
}
