using FluentValidation;
using InnoGotchi.BusinessLogic.SessionEntities;
using Microsoft.AspNetCore.Http;

namespace InnoGotchi.BusinessLogic.Extensions
{
    public static class SessionFarmExtension
    {
        public static SessionFarm GetFarmFromSession(this HttpContext httpContext)
        {
            return httpContext.Session.Get<SessionFarm>("SessionFarm");
        }

        public static async Task SetSessionFarmData(this HttpContext httpContext, SessionFarm farm)
        {
            httpContext.Session.Set<SessionFarm>("SessionFarm", farm);
        }

        public static async Task<bool> IsValidSessionFarm(this HttpContext context, IValidator<SessionFarm> validator)
        {
            var farm = context.Session.Get<SessionFarm>("SessionFarm");
            var validateResult = await validator.ValidateAsync(farm);
            return validateResult.IsValid;
        }
    }
}
