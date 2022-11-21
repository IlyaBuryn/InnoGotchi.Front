using InnoGotchi.Http.Components;
using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace InnoGotchi.Http.HttpServices
{
    public class BodyPartService : IBodyPartService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private HttpServiceHelper<BodyPartModel> _httpServiceHelper;

        public BodyPartService(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

            _httpServiceHelper = new HttpServiceHelper<BodyPartModel>(_configuration, _httpClient, _httpContextAccessor);
        }

        public async Task<ResponseModel<List<BodyPartModel>>> GetBodyParts()
        {
            return await _httpServiceHelper.Get<List<BodyPartModel>>("body-parts");
        }

        public async Task<ResponseModel<List<BodyPartModel>>> GetBodyPartsByTypeId(int typeId)
        {
            return await _httpServiceHelper.Get<List<BodyPartModel>>($"body-parts/type/{typeId}");
        }
    }
}
