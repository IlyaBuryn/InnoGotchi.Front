using InnoGotchi.DataAccess.Models.ResponseModels;
using System.Runtime.CompilerServices;

namespace InnoGotchi.DataAccess.Extensions
{
    public static class ResponseModelExtensions
    {
        public static string ValueIsNullError<T>(this ResponseModel<T> responseModel, string valueName) 
            => $"Client: Value ({nameof(valueName)}) in Response model is null!";

        public static string ValueIsNullError<T>(this ResponseModel<T> responseModel)
            => $"Client: Value ({nameof(responseModel.Value)}) in Response model is null!";

        public static string HttpContextError<T>(this ResponseModel<T> responseModel, string error) 
            => $"Http context accessor error: \"{error}\"";
    }
}
