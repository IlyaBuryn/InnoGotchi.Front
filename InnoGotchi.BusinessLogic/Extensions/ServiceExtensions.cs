using Microsoft.Extensions.DependencyInjection;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.BusinessLogic.Services;
using InnoGotchi.DataAccess.Extensions;

namespace InnoGotchi.BusinessLogic.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddBusinessLogicServices(this IServiceCollection builder)
        {
            BusinessLogicServicesConfiguration(builder);
            builder.AddAPIClients();
        }


        private static void BusinessLogicServicesConfiguration(IServiceCollection service)
        {
            service.AddScoped<IAccountService, AccountService>();
            service.AddScoped<ICollabService, CollabService>();
            service.AddScoped<IFarmService, FarmService>();
            service.AddScoped<IPetService, PetService>();
        }
    }
}
