using InnoGotchi.BusinessLogic.SessionEntities;
using InnoGotchi.BusinessLogic.BusinessModels;
using Microsoft.AspNetCore.Http;

namespace InnoGotchi.BusinessLogic.Extensions
{
    public static class SessionUserExtensions
    {

        public static SessionUser GetUserFromSession(this HttpContext httpContext)
        {
            var sessionUser = httpContext.Session.Get<SessionUser>("SessionUser");
            if (sessionUser == null)
            {
                return default;
            }
            return sessionUser;
        }

        public static void UpdateUserInfo(this SessionUser user, UserUpdateModel model)
        {
            user.Name = model.Name;
            if (!string.IsNullOrEmpty(model.Surname))
            {
                user.Surname = model.Surname;
            }
            if (model.Image != null)
            {
                user.Image = model.Image;
            }
        }

        public static void SetSessionUserData(this HttpContext httpContext, SessionUser user)
        {
            httpContext.Session.Set<SessionUser>("SessionUser", user);
        }

        public static void Logout(this HttpContext httpContext)
        {
            httpContext.Session.Remove("SessionUser");
            httpContext.Session.Remove("SessionFarm");
        }
    }
}
