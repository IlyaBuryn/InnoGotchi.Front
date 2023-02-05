using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.DataAccess.Interfaces
{
    public interface ICustomHttpClient<T>
    {
        public Task<ResponseModel<TypeT>> Get<TypeT>(string path);
        public Task<ResponseModel<TypeT>> Put<TypeT>(T model, string path);
        public Task<ResponseModel<TypeT>> Post<TypeT>(T model, string path);
        public Task<ResponseModel<TypeT>> Delete<TypeT>(string path);

    }
}
