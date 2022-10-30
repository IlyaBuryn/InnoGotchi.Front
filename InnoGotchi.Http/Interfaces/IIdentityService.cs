using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.Interfaces
{
    public interface IIdentityService
    {
        public Task<AuthResponseModel> SignIn(LoginModel request);
        public Task<AuthResponseModel> SignUp(RegisterModel request);
    }
}
