using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.DataAccess.Interfaces.HttpClients
{
    public interface IFeedClient
    {
        public Task<ResponseModel<int?>> FeedPet(FeedDto request);
        public Task<ResponseModel<int?>> DrinkPet(FeedDto request);
        public Task RecalculatePetsNeeds(int farmId);
        public Task<ResponseModel<double?>> FeedingFoodPeriod(int farmId);
        public Task<ResponseModel<double?>> FeedingDrinkPeriod(int farmId);
    }
}
