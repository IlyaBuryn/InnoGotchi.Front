using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.HttpClients;
using InnoGotchi.DataAccess.Interfaces;
using InnoGotchi.DataAccess.Interfaces.HttpClients;
using Microsoft.Extensions.DependencyInjection;

namespace InnoGotchi.DataAccess.Extensions
{
    public static class DataAccessExtensions
    {
        public static void ConfigureDataAccessLayer(this IServiceCollection builder)
        {
            ConfigureClients(builder);
            ConfigureCustomHttpClient(builder);
        }


        private static void ConfigureClients(IServiceCollection services)
        {

            services.AddScoped<IIdentityClient, IdentityClient>();
            services.AddScoped<IFarmClient, FarmClient>();
            services.AddScoped<IPetClient, PetClient>();
            services.AddScoped<IBodyPartClient, BodyPartClient>();
            services.AddScoped<ICollabClient, CollabClient>();
            services.AddScoped<IFeedClient, FeedClient>();
        }


        private static void ConfigureCustomHttpClient(IServiceCollection services)
        {
            services.AddScoped<ICustomHttpClient<AuthenticateRequestDto>, CustomHttpClient<AuthenticateRequestDto>>();
            services.AddScoped<ICustomHttpClient<IdentityUserDto>, CustomHttpClient<IdentityUserDto>>();
            services.AddScoped<ICustomHttpClient<FarmDto>, CustomHttpClient<FarmDto>>();
            services.AddScoped<ICustomHttpClient<PetDto>, CustomHttpClient<PetDto>>();
            services.AddScoped<ICustomHttpClient<BodyPartDto>, CustomHttpClient<BodyPartDto>>();
            services.AddScoped<ICustomHttpClient<CollaboratorDto>, CustomHttpClient<CollaboratorDto>>();
            services.AddScoped<ICustomHttpClient<FeedDto>, CustomHttpClient<FeedDto>>();
        }
    }
}