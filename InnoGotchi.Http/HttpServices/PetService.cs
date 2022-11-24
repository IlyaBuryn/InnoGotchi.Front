using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;

namespace InnoGotchi.Http.HttpServices
{
    public class PetService : IPetService
    {
        private readonly IHttpClientService<PetDto> _httpServiceHelper;

        public PetService(IHttpClientService<PetDto> httpServiceHelper)
        {
            _httpServiceHelper = httpServiceHelper;
        }

        public async Task<ResponseModel<int?>> CreatePet(PetDto request)
        {
            return await _httpServiceHelper.Post<int?>(request, "pet/create");
        }

        public async Task<ResponseModel<List<PetDto>>> GetAllPets(int page, SortFilter sortFilter)
        {
            return await _httpServiceHelper.Get<List<PetDto>>($"pet/page/{page}/{(int)sortFilter}");
        }

        public async Task<ResponseModel<int?>> GetAllPetsCount()
        {
            return await _httpServiceHelper.Get<int?>("pet/all-count");
        }

        public async Task<ResponseModel<PetDto>> GetPetById(int petId)
        {
            return await _httpServiceHelper.Get<PetDto>($"pet/{petId}");
        }

        public async Task<ResponseModel<List<PetDto>>> GetPetsByFarmId(int farmId)
        {
            return await _httpServiceHelper.Get<List<PetDto>>($"pet/farm/{farmId}");
        }
    }
}
