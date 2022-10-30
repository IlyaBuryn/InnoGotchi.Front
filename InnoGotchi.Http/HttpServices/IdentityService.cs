using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;
using InnoGotchi.Http.Responses;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace InnoGotchi.Http.HttpServices
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public IdentityService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<AuthResponseModel> SignIn(LoginModel request)
        {

            var uri = _configuration.GetSection("InnoGotchiAPI").Value + "Account/auth/";
            var content = new StringContent(JsonConvert.SerializeObject(request),
                    Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);
            var apiResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<AuthResponseModel>(apiResponse);
                return result;
            }

            var errorMessage = JsonConvert.DeserializeObject<ErrorResponse>(apiResponse);
            var errorResult = new AuthResponseModel() { 
                ErrorMessage = errorMessage.Errors.FirstOrDefault() };
            return errorResult;
        }

        public async Task<AuthResponseModel> SignUp(RegisterModel request)
        {
            var uri = _configuration.GetSection("InnoGotchiAPI").Value + "Account/register/";
            var content = new StringContent(JsonConvert.SerializeObject(request),
                    Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);
            var apiResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<AuthResponseModel>(apiResponse);
                return result;
            }

            var errorMessage = JsonConvert.DeserializeObject<ErrorResponse>(apiResponse);
            var errorResult = new AuthResponseModel()
            {
                ErrorMessage = errorMessage.Errors.FirstOrDefault()
            };
            return errorResult;
        }
    }
}
