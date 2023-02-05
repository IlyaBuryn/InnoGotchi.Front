using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.DataAccess.Interfaces.HttpClients
{
    public interface IPetClient
    {
        public Task<ResponseModel<int?>> CreatePet(PetDto request);
        public Task<ResponseModel<PetDto>> GetPetById(int petId);
        public Task<ResponseModel<List<PetDto>>> GetPetsByFarmId(int farmId);
        public Task<ResponseModel<List<PetDto>>> GetAllPets(int page, SortFilter sortType);
        public Task<ResponseModel<int?>> GetAllPetsCount();
    }
}
