using InnoGotchi.Components.DtoModels;
using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.HttpServices
{
    public class BodyPartService : IBodyPartService
    {
        private readonly IHttpClientService<BodyPartDto> _httpServiceHelper;

        public BodyPartService(IHttpClientService<BodyPartDto> httpClientService)
        {
            _httpServiceHelper = httpClientService;
        }

        public async Task<ResponseModel<List<BodyPartDto>>> GetBodyParts()
        {
            return await _httpServiceHelper.Get<List<BodyPartDto>>("body-parts");
        }

        public async Task<ResponseModel<List<BodyPartDto>>> GetBodyPartsByTypeId(int typeId)
        {
            return await _httpServiceHelper.Get<List<BodyPartDto>>($"body-parts/type/{typeId}");
        }
    }
}
