using FluentValidation;
using InnoGotchi.Http.Models;

namespace InnoGotchi.WEB.Utility
{
    public static class SessionUserExtension
    {
        public static async Task SetSessionUserData(this HttpContext httpContext, SessionUser user, IValidator<SessionUser> validator)
        {
            var validateResult = await validator.ValidateAsync(user);
            if (validateResult.IsValid)
                httpContext.Session.Set<SessionUser>("SessionUser", user);
        }

        public static async Task SetSessionFarmData(this HttpContext httpContext, SessionFarm farm, IValidator<SessionFarm> validator)
        {
            var validateResult = await validator.ValidateAsync(farm);
            if (validateResult.IsValid)
                httpContext.Session.Set<SessionFarm>("SessionFarm", farm);
        }

        public static void Logout(this HttpContext httpContext)
        {
            httpContext.Session.Set<SessionUser>("SessionUser", null);
            httpContext.Session.Set<SessionFarm>("SessionFarm", null);
        }


        public static void SetUserModel(out IdentityUserModel userInfo, SessionUser user)
        {
            userInfo = new IdentityUserModel();
            userInfo.Id = user.Id;
            userInfo.Username = user.Username;
            userInfo.Name = user.Name;
            userInfo.Surname = user.Surname;
            userInfo.Image = user.Image;
            userInfo.Role = user.Role;
            userInfo.Token = user.Token;
        }

        public static async Task<bool> IsValidSessionUser(this HttpContext context, IValidator<SessionUser> validator)
        {
            var user = context.Session.Get<SessionUser>("SessionUser");
            var validateResult = await validator.ValidateAsync(user);
            return validateResult.IsValid;   
        }

        public static async Task<bool> IsValidSessionFarm(this HttpContext context, IValidator<SessionFarm> validator)
        {
            var farm = context.Session.Get<SessionFarm>("SessionFarm");
            var validateResult = await validator.ValidateAsync(farm);
            return validateResult.IsValid;
        }
    }
}
