using FluentValidation;
using FluentValidation.Results;
using InnoGotchi.BusinessLogic.SessionEntities;
using InnoGotchi.BusinessLogic.Validation;
using InnoGotchi.DataAccess.Models.ResponseModels;
using Microsoft.Extensions.DependencyInjection;

namespace InnoGotchi.WEB.Extensions
{
    public static class ValidationExtensions
    {
        public static void AddDataValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<SessionUser>, SessionUserValidator>();
            services.AddTransient<IValidator<SessionFarm>, SessionFarmValidator>();
        }

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
