using InnoGotchi.Http.Models;

namespace InnoGotchi.WEB.Utility
{
    public static class UserHelpers
    {

        public static void KeepUserInData(this HttpContext httpContext, AuthResponseModel response)
        {
            httpContext.Session.Set("UserId", response.Id);
            httpContext.Session.Set("Username", response.Username);
            httpContext.Session.Set("Name", response.Name);
            httpContext.Session.Set("Surname", response.Surname);
            httpContext.Session.Set("UserRole", response.Role);
            httpContext.Session.Set("ImagePath", response.Image ?? "~/images/userTemp.png");
            httpContext.Session.Set("UserToken", response.Token);
        }

        public static void Logout(this HttpContext httpContext)
        {
            httpContext.Session.Remove("UserId");
            httpContext.Session.Remove("Username");
            httpContext.Session.Remove("Name");
            httpContext.Session.Remove("Surname");
            httpContext.Session.Remove("UserRole");
            httpContext.Session.Remove("ImagePath");
            httpContext.Session.Remove("UserToken");
        }

        public static void SetUserModel(this HttpContext httpContext, out IdentityUserModel userInfo)
        {
            userInfo = new IdentityUserModel();
            userInfo.Id = httpContext.Session.Get<int>("UserId");
            userInfo.Username = httpContext.Session.Get<string>("Username");
            userInfo.Name = httpContext.Session.Get<string>("Name");
            userInfo.Surname = httpContext.Session.Get<string>("Surname");
            userInfo.Image = httpContext.Session.Get<string>("ImagePath");
            userInfo.Role = httpContext.Session.Get<string>("UserRole");
            userInfo.Token = httpContext.Session.Get<string>("UserToken");
        }
    }
}
