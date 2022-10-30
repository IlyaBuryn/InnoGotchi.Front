using InnoGotchi.Http.HttpServices;
using InnoGotchi.Http.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InnoGotchi.Http.Components
{
    public static class ConfigurationHttpModule
    {
        private static void ServiceConfiguration(IServiceCollection service)
        {
            service.AddScoped<IIdentityService, IdentityService>();
        }

        public static void ConfigurationHttpServices(this IServiceCollection builder)
        {
            ServiceConfiguration(builder);
        }
    }
}