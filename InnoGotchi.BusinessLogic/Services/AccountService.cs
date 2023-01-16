using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using InnoGotchi.DataAccess.Models.IdentityModels;
using FluentValidation;
using InnoGotchi.BusinessLogic.SessionEntities;
using InnoGotchi.DataAccess.Models.ResponseModels;
using InnoGotchi.BusinessLogic.BusinessModels;
using InnoGotchi.BusinessLogic.Extensions;
using InnoGotchi.DataAccess.Interfaces.HttpClients;

namespace InnoGotchi.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityClient _identityClient;
        private readonly IWebHostEnvironment _webEnv;
        private readonly IValidator<SessionUser> _sessionUserValidator;


        public AccountService(IHttpContextAccessor httpContextAccessor, 
            IIdentityClient identityClient, 
            IWebHostEnvironment webEnv, 
            IValidator<SessionUser> sessionUserValidator)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityClient = identityClient;
            _webEnv = webEnv;
            _sessionUserValidator = sessionUserValidator;
        }


        public async Task<ResponseModel<bool>> ChangePassword(PasswordUpdateModel model)
        {
            var result = new ResponseModel<bool>();

            var user = _httpContextAccessor.HttpContext.GetUserFromSession();

            var loginModel = new LoginModel() { Username = user.Username, Password = model.OldPassword };
            var loginResponse = await _identityClient.SignIn(loginModel);
            if (loginResponse.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(loginResponse.Error);

            var request = new IdentityUserModel()
            {
                Id = model.UserId,
                Username = user.Username,
                Password = model.NewPassword,
                Token = user.Token
            };

            var updateResponse = await _identityClient.UpdatePassword(request);
            if (updateResponse.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(updateResponse.Error);

            result.Value = (bool)updateResponse.Value;
            return result;
        }


        public async Task<ResponseModel<bool>> ChangeUserInfo(UserUpdateModel model, IFormFile image)
        {
            var result = new ResponseModel<bool>();

            var user = _httpContextAccessor.HttpContext.GetUserFromSession();

            var imagePath = string.Empty;
            if (image != null)
            {
                var name = Path.Combine(_webEnv.WebRootPath + "/images", Path.GetFileName(image.FileName));
                using (var stream = new FileStream(name, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                    imagePath = "~/images/" + image.FileName;
                }
            }
            else
            {
                imagePath = user.Image;
            }

            var userModel = new IdentityUserModel()
            {
                Id = model.UserId,
                Username = user.Username,
                Token = user.Token,
                Name = model.Name,
                Surname = model.Surname,
                Image = imagePath,
            };

            var updateResponse = await _identityClient.UpdateUserInfo(userModel);
            if (updateResponse.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(updateResponse.Error);

            result.Value = (bool)updateResponse.Value;
            user.UpdateUserInfo(model, imagePath);
            return result;
        }


        public async Task<ResponseModel<AuthenticateResponseDto>> SignIn(AuthRequestModel model)
        {
            var result = new ResponseModel<AuthenticateResponseDto>();

            var loginModel = new LoginModel()
            {
                Username = model.Username,
                Password = model.Password,
        };

            var loginResponse = await _identityClient.SignIn(loginModel);
            if (loginResponse.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(loginResponse.Error);

            result.Value = loginResponse.Value;
            return result;
        }


        public async Task<ResponseModel<AuthenticateResponseDto>> SignUp(AuthRequestModel model)
        {
            var result = new ResponseModel<AuthenticateResponseDto>();

            var registerModel = new RegisterModel()
            {
                Username = model.Username,
                Password = model.Password,
                Name = model.Name,
                Surname = model.Surname,
                Image = model.Image,
                IdentityRoleId = model.IdentityRoleId ?? 0,
            };

            var registerResponse = await _identityClient.SignUp(registerModel);
            if (registerResponse.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(registerResponse.Error);

            result.Value = registerResponse.Value;
            return result;
        }
    }
}
