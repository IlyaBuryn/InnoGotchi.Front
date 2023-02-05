using FluentValidation.Results;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.WEB.Extensions
{
    public static class ValidationExtensions
    {
        public static bool IsValidResult<T>(this ValidationResult validationResult, ResponseModel<T> result)
        {
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    result.Errors.Add(error.ErrorMessage);
                }
                return false;
            }
            return true;
        }
    }
}
