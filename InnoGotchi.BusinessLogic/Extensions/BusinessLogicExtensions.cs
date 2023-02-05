using FluentValidation;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.BusinessLogic.MapProfiles;
using InnoGotchi.BusinessLogic.Services;
using InnoGotchi.BusinessLogic.SessionEntities;
using InnoGotchi.BusinessLogic.Validation;
using InnoGotchi.Components.Extensions;
using InnoGotchi.DataAccess.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InnoGotchi.BusinessLogic.Extensions
{
    public static class BusinessLogicExtensions
    {
        public static void ConfigureBusinessLogicLayer(this IServiceCollection builder)
        {
            ConfigureMapProfiles(builder);
            ConfigureSessionValidators(builder);
            ConfigureDtoValidators(builder);
            ConfigureServices(builder);

            builder.ConfigureDataAccessLayer();
        }

        private static void ConfigureMapProfiles(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<SignInMapProfile>();
                cfg.AddProfile<SignUpMapProfile>();
                cfg.AddProfile<UserUpdateMapProfile>();
                cfg.AddProfile<PasswordUpdateMapProfile>();
            });
        }

        private static void ConfigureSessionValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<SessionUser>, SessionUserValidator>();
            services.AddTransient<IValidator<SessionFarm>, SessionFarmValidator>();
        }

        private static void ConfigureDtoValidators(IServiceCollection services)
        {
            var assembly = Assembly.Load(new AssemblyName("InnoGotchi.Components"));
            services.AddValidatorsFromAssembly(assembly);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICollabService, CollabService>();
            services.AddScoped<IFarmService, FarmService>();
            services.AddScoped<IPetService, PetService>();
        }
    }
}
