using InnoGotchi.Http.Models;
using InnoGotchi.Http.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace InnoGotchi.Http.Components
{
    public class HttpServiceHelper<T>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public HttpServiceHelper(IConfiguration configuration, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            InitialRequestHeader();
        }

        public async Task<ResponseModel<T>> Get(string path)
        {
            try
            {
                var uri = MakeUri(path);
                var response = await _httpClient.GetAsync(uri);
                var apiResponse = await ConvertResponse(response);
                return CheckResponse<T>(response, apiResponse);
            }
            catch (Exception ex)
            {
                return new ResponseModel<T>(default(T), ex.Message);
            }
        }

        public async Task<ResponseModel<TypeT>> Get<TypeT>(string path)
        {
            try
            {
                var uri = MakeUri(path);
                var response = await _httpClient.GetAsync(uri);
                var apiResponse = await ConvertResponse(response);
                return CheckResponse<TypeT>(response, apiResponse);
            }
            catch (Exception ex)
            {
                return new ResponseModel<TypeT>(default(TypeT), ex.Message);
            }
        }

        public async Task<ResponseModel<TypeT>> Put<TypeT>(T model, string path)
        {
            try
            {
                var uri = MakeUri(path);
                var content = MakeContent(model);
                var response = await _httpClient.PutAsync(uri, content);
                var apiResponse = await ConvertResponse(response);
                return CheckResponse<TypeT>(response, apiResponse);
            }
            catch (Exception ex)
            {
                return new ResponseModel<TypeT>(default(TypeT), ex.Message);
            }
        }

        public async Task<ResponseModel<TypeT>>Post<TypeT>(T model, string path)
        {
            try
            {
                var uri = MakeUri(path);
                var content = MakeContent(model);
                var response = await _httpClient.PostAsync(uri, content);
                var apiResponse = await ConvertResponse(response);
                return CheckResponse<TypeT>(response, apiResponse);
            }
            catch (Exception ex)
            {
                return new ResponseModel<TypeT>(default(TypeT), ex.Message);
            }
        }

        public async Task<ResponseModel<TypeT>>Delete<TypeT>(string path)
        {
            try
            {
                var uri = MakeUri(path);
                var response = await _httpClient.DeleteAsync(uri);
                var apiResponse = await ConvertResponse(response);
                return CheckResponse<TypeT>(response, apiResponse);
            }
            catch (Exception ex)
            {
                return new ResponseModel<TypeT>(default(TypeT), ex.Message);
            }
        }




        private string MakeUri(string path)
        {
            return _configuration.GetSection("InnoGotchiAPI").Value + path;
        }

        private async Task<string> ConvertResponse(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }

        private ResponseModel<U>CheckResponse<U>(HttpResponseMessage response, string apiResponse)
        {
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<U>(apiResponse);
                return new ResponseModel<U>(result);
            }

            var errorMessage = JsonConvert.DeserializeObject<ErrorResponse>(apiResponse);
            return new ResponseModel<U>(default(U), errorMessage?.Errors?.FirstOrDefault());
        } 

        private StringContent MakeContent(T model)
        {
            return new StringContent(JsonConvert.SerializeObject(model),
                        Encoding.UTF8, "application/json");
        }

        private void InitialRequestHeader()
        {
            string token = _httpContextAccessor.HttpContext.Request.Cookies["UserJwtToken"];
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
    }
}
