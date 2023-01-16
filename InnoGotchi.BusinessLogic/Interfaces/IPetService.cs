using InnoGotchi.BusinessLogic.BusinessModels;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface IPetService
    {
        public Task<ResponseModel<int?>> CreatePet(PetDto pet);
        public Task<ResponseModel<int?>> FeedPet(FeedDto feedDto);
        public Task<ResponseModel<int?>> DrinkPet(FeedDto feedDto);
        public Task<ResponseModel<PetDto>> PetDetails(int petId);
        public Task<ResponseModel<Dictionary<string, List<BodyPartDto>>>> BodyPartsForCreatingPetView();
        public Task<ResponseModel<PageModel>> PetPage(int pageNum, SortFilter sortFilter);
    }
}
