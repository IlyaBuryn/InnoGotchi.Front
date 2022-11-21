using InnoGotchi.Http.Components.Enums;
using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.Interfaces
{
    public interface IPetService
    {
        public Task<ResponseModel<int?>> CreatePet(PetModel request);
        public Task<ResponseModel<PetModel>> GetPetById(int petId);
        public Task<ResponseModel<List<PetModel>>> GetPetsByFarmId(int farmId);
        public Task<ResponseModel<List<PetModel>>> GetAllPets(int page, SortFilter sortType);
        public Task<ResponseModel<int?>> GetAllPetsCount();
    }
}
