using InnoGotchi.Components.DtoModels;
using InnoGotchi.Http.HttpServices;
using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;
using Microsoft.Extensions.DependencyInjection;

namespace InnoGotchi.Http.Components
{
    public static class ConfigurationHttpModule
    {
        private static void HttpServiceConfiguration(IServiceCollection service)
        {
            
            service.AddScoped<IIdentityService, IdentityService>();
            service.AddScoped<IFarmService, FarmService>();
            service.AddScoped<IPetService, PetService>();
            service.AddScoped<IBodyPartService, BodyPartService>();
            service.AddScoped<ICollabService, CollabService>();
            service.AddScoped<IFeedService, FeedService>();
        }

        private static void HttpClientServiceConfiguration(IServiceCollection service)
        {
            service.AddScoped<IHttpClientService<IdentityUserModel>, HttpClientService<IdentityUserModel>>();
            service.AddScoped<IHttpClientService<RegisterModel>, HttpClientService<RegisterModel>>();
            service.AddScoped<IHttpClientService<LoginModel>, HttpClientService<LoginModel>>();
            service.AddScoped<IHttpClientService<FarmDto>, HttpClientService<FarmDto>>();
            service.AddScoped<IHttpClientService<PetDto>, HttpClientService<PetDto>>();
            service.AddScoped<IHttpClientService<BodyPartDto>, HttpClientService<BodyPartDto>>();
            service.AddScoped<IHttpClientService<CollaboratorDto>, HttpClientService<CollaboratorDto>>();
            service.AddScoped<IHttpClientService<FeedDto>, HttpClientService<FeedDto>>();
        }

        public static void ConfigurationHttpServices(this IServiceCollection builder)
        {
            HttpServiceConfiguration(builder);
            HttpClientServiceConfiguration(builder);
        }
    }
}