using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using InnoGotchi.Http.Components;
using InnoGotchi.Http.Components.Enums;

namespace InnoGotchi.Http.HttpServices
{
    public class PetService : IPetService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly HttpServiceHelper<PetModel> _petServiceHelper;

        public PetService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            _petServiceHelper = new HttpServiceHelper<PetModel>(_configuration, _httpClient, _httpContextAccessor);
        }

        public async Task<ResponseModel<int?>> CreatePet(PetModel request)
        {
            return await _petServiceHelper.Post<int?>(request, "pet/create");
        }

        public async Task<ResponseModel<List<PetModel>>> GetAllPets(int page, SortFilter sortFilter)
        {
            return await _petServiceHelper.Get<List<PetModel>>($"pet/page/{page}/{(int)sortFilter}");
        }

        public async Task<ResponseModel<int?>> GetAllPetsCount()
        {
            return await _petServiceHelper.Get<int?>("pet/all-count");
        }

        public async Task<ResponseModel<PetModel>> GetPetById(int petId)
        {
            return await _petServiceHelper.Get<PetModel>($"pet/{petId}");
        }

        public async Task<ResponseModel<List<PetModel>>> GetPetsByFarmId(int farmId)
        {
            return await _petServiceHelper.Get<List<PetModel>>($"pet/farm/{farmId}");
        }
    }
}
