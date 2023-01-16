using InnoGotchi.BusinessLogic.SessionEntities;
using FluentValidation;
using InnoGotchi.DataAccess.Models.IdentityModels;
using InnoGotchi.BusinessLogic.BusinessModels;
using Microsoft.AspNetCore.Http;

namespace InnoGotchi.BusinessLogic.Extensions
{
    public static class SessionUserExtensions
    {

        public static SessionUser GetUserFromSession(this HttpContext httpContext)
        {
            return httpContext.Session.Get<SessionUser>("SessionUser");
        }

        public static void UpdateUserInfo(this SessionUser user, UserUpdateModel model, string imagePath)
        {
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Image = imagePath;
        }

        public static async Task SetSessionUserData(this HttpContext httpContext, SessionUser user)
        {
            httpContext.Session.Set<SessionUser>("SessionUser", user);
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
    }
}
