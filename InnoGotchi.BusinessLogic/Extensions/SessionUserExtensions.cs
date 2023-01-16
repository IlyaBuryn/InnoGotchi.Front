using InnoGotchi.BusinessLogic.SessionEntities;
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

        public static void UpdateUserInfo(this SessionUser user, UserUpdateModel model)
        {
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Image = model.Image;
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
    }
}
