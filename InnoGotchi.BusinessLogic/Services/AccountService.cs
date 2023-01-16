using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using FluentValidation;
using InnoGotchi.BusinessLogic.SessionEntities;
using InnoGotchi.DataAccess.Models.ResponseModels;
using InnoGotchi.BusinessLogic.BusinessModels;
using InnoGotchi.BusinessLogic.Extensions;
using InnoGotchi.DataAccess.Interfaces.HttpClients;
using AutoMapper;

namespace InnoGotchi.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityClient _identityClient;
        private readonly IWebHostEnvironment _webEnv;
        private readonly IValidator<SessionUser> _sessionUserValidator;
        private readonly IMapper _mapper;


        public AccountService(IHttpContextAccessor httpContextAccessor, 
            IIdentityClient identityClient, 
            IWebHostEnvironment webEnv, 
            IValidator<SessionUser> sessionUserValidator,
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _identityClient = identityClient;
            _webEnv = webEnv;
            _sessionUserValidator = sessionUserValidator;
            _mapper = mapper;
        }


        public async Task<ResponseModel<bool>> ChangePassword(PasswordUpdateModel model)
        {
            var result = new ResponseModel<bool>();

            var user = _httpContextAccessor.HttpContext.GetUserFromSession();

            var loginResponse = await _identityClient.SignIn(_mapper.Map<AuthenticateRequestDto>(model));
            if (loginResponse.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(loginResponse.Error);

            var updateResponse = await _identityClient.UpdatePassword(_mapper.Map<IdentityUserDto>(model));
            if (updateResponse.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(updateResponse.Error);

            result.Value = (bool)updateResponse.Value;
            return result;
        }


        public async Task<ResponseModel<UserUpdateModel>> ChangeUserInfo(UserUpdateModel model, IFormFile image)
        {
            var result = new ResponseModel<UserUpdateModel>();

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

            model.Image = imagePath;

            var updateResponse = await _identityClient.UpdateUserInfo(_mapper.Map<IdentityUserDto>(model));
            if (updateResponse.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(updateResponse.Error);

            result.Value = model;
            return result;
        }


        public async Task<ResponseModel<AuthenticateResponseDto>> SignIn(AuthRequestModel model)
        {
            var result = new ResponseModel<AuthenticateResponseDto>();

            var loginResponse = await _identityClient.SignIn(_mapper.Map<AuthenticateRequestDto>(model));
            if (loginResponse.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(loginResponse.Error);

            result.Value = loginResponse.Value;
            return result;
        }


        public async Task<ResponseModel<AuthenticateResponseDto>> SignUp(AuthRequestModel model)
        {
            var result = new ResponseModel<AuthenticateResponseDto>();

            var registerResponse = await _identityClient.SignUp(_mapper.Map<IdentityUserDto>(model));
            if (registerResponse.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(registerResponse.Error);

            result.Value = registerResponse.Value;
            return result;
        }
    }
}
