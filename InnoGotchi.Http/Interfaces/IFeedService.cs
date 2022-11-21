using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.Interfaces
{
    public interface IFeedService
    {
        public Task<ResponseModel<int?>> FeedPet(FeedModel request);
        public Task<ResponseModel<int?>> DrinkPet(FeedModel request);
        public Task RecalculatePetsNeeds(int farmId);
        public Task<ResponseModel<double?>> FeedingFoodPeriod(int farmId);
        public Task<ResponseModel<double?>> FeedingDrinkPeriod(int farmId);
    }
}
