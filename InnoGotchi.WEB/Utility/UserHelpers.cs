using InnoGotchi.Http.Models;

namespace InnoGotchi.WEB.Utility
{
    public static class UserHelpers
    {
        public static void KeepUserInData(this HttpContext httpContext, AuthResponseModel response)
        {
            httpContext.Session.Set("Id", response.Id);
            httpContext.Session.Set("Username", response.Username?.Replace("\"", ""));
            httpContext.Session.Set("Name", response.Name?.Replace("\"", ""));
            httpContext.Session.Set("Surname", response.Surname?.Replace("\"", ""));
            httpContext.Session.Set("UserRole", response.Role?.Replace("\"", ""));
            httpContext.Session.Set("UserToken", response.Token?.Replace("\"", ""));
        }

        public static void Logout(this HttpContext httpContext)
        {
            httpContext.Session.Remove("Id");
            httpContext.Session.Remove("Username");
            httpContext.Session.Remove("Name");
            httpContext.Session.Remove("Surname");
            httpContext.Session.Remove("UserRole");
            httpContext.Session.Remove("UserToken");
        }
    }
}
