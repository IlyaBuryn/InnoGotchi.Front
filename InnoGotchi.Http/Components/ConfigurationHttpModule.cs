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
            service.AddScoped<IFarmService, FarmService>();
            service.AddScoped<IPetService, PetService>();
            service.AddScoped<IBodyPartService, BodyPartService>();
            service.AddScoped<ICollabService, CollabService>();
            service.AddScoped<IFeedService, FeedService>();
        }

        public static void ConfigurationHttpServices(this IServiceCollection builder)
        {
            ServiceConfiguration(builder);
        }
    }
}