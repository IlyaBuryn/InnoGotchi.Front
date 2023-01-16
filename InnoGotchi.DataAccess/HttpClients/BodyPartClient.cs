using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Interfaces.HttpClients;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.DataAccess.HttpClients
{
    public class BodyPartClient : IBodyPartClient
    {
        private readonly ICustomHttpClient<BodyPartDto> _httpServiceHelper;

        public BodyPartClient(ICustomHttpClient<BodyPartDto> httpClientService)
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
