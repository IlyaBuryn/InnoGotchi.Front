using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.HttpClients;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Interfaces.HttpClients;
using InnoGotchi.DataAccess.Models.IdentityModels;
using Microsoft.Extensions.DependencyInjection;

namespace InnoGotchi.DataAccess.Extensions
{
    public static class APIClientExtensions
    {
        public static void AddAPIClients(this IServiceCollection builder)
        {
            APIRequestsConfiguration(builder);
            APIClientsConfiguration(builder);
        }


        private static void APIRequestsConfiguration(IServiceCollection service)
        {

            service.AddScoped<IIdentityClient, IdentityClient>();
            service.AddScoped<IFarmClient, FarmClient>();
            service.AddScoped<IPetClient, PetClient>();
            service.AddScoped<IBodyPartClient, BodyPartClient>();
            service.AddScoped<ICollabClient, CollabClient>();
            service.AddScoped<IFeedClient, FeedClient>();
        }

        private static void APIClientsConfiguration(IServiceCollection service)
        {
            service.AddScoped<ICustomHttpClient<IdentityUserModel>, CustomHttpClient<IdentityUserModel>>();
            service.AddScoped<ICustomHttpClient<RegisterModel>, CustomHttpClient<RegisterModel>>();
            service.AddScoped<ICustomHttpClient<LoginModel>, CustomHttpClient<LoginModel>>();
            service.AddScoped<ICustomHttpClient<FarmDto>, CustomHttpClient<FarmDto>>();
            service.AddScoped<ICustomHttpClient<PetDto>, CustomHttpClient<PetDto>>();
            service.AddScoped<ICustomHttpClient<BodyPartDto>, CustomHttpClient<BodyPartDto>>();
            service.AddScoped<ICustomHttpClient<CollaboratorDto>, CustomHttpClient<CollaboratorDto>>();
            service.AddScoped<ICustomHttpClient<FeedDto>, CustomHttpClient<FeedDto>>();
        }
    }
}