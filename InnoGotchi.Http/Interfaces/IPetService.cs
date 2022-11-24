using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;
using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.Interfaces
{
    public interface IPetService
    {
        public Task<ResponseModel<int?>> CreatePet(PetDto request);
        public Task<ResponseModel<PetDto>> GetPetById(int petId);
        public Task<ResponseModel<List<PetDto>>> GetPetsByFarmId(int farmId);
        public Task<ResponseModel<List<PetDto>>> GetAllPets(int page, SortFilter sortType);
        public Task<ResponseModel<int?>> GetAllPetsCount();
    }
}
