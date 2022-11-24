using FluentValidation;
using InnoGotchi.WEB.Validation;

namespace InnoGotchi.WEB.Utility
{
    public static class ValidationExtension
    {
        public static void AddDataValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<SessionUser>, SessionUserValidator>();
            services.AddTransient<IValidator<SessionFarm>, SessionFarmValidator>();
        }
    }
}
