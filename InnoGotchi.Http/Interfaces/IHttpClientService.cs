using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.Interfaces
{
    public interface IHttpClientService<T>
    {
        public Task<ResponseModel<T>> Get(string path);
        public Task<ResponseModel<TypeT>> Get<TypeT>(string path);
        public Task<ResponseModel<TypeT>> Put<TypeT>(T model, string path);
        public Task<ResponseModel<TypeT>> Post<TypeT>(T model, string path);
        public Task<ResponseModel<TypeT>> Delete<TypeT>(string path);

    }
}
