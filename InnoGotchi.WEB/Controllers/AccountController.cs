using FluentValidation;
using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;
using InnoGotchi.WEB.Models;
using InnoGotchi.WEB.Utility;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly IWebHostEnvironment _webEnv;
        private readonly IValidator<SessionUser> _validator;

        public AccountController(IIdentityService identityService,
            IWebHostEnvironment webEnv,
            IValidator<SessionUser> validator)
        {
            _identityService = identityService;
            _validator = validator;
            _webEnv = webEnv;
        }

        public IActionResult Index(bool isAuth)
        {
            HttpContext.Session.Set("IsAuthorization", isAuth);
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Logout();
            HttpContext.SetModalMessage("You have successfully logout", ModalMsgType.JustMessage);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Settings()
        {
            var user = HttpContext.Session.Get<SessionUser>("SessionUser");
            return (user != null) ? View(user) : this.ReturnDueToError(HttpContext, toController: "Account");
        }

        public IActionResult ChangeUserInfo()
        {
            var user = HttpContext.Session.Get<SessionUser>("SessionUser");
            return (user != null) ? View(new UserUpdateModel(user)) : this.ReturnDueToError(HttpContext);
        }

        public IActionResult ChangePasswordInfo()
        {
            var user = HttpContext.Session.Get<SessionUser>("SessionUser");
            return (user != null) ? View(new PasswordUpdateModel(user)) : this.ReturnDueToError(HttpContext);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(AuthRequestModel model)
        {
            var loginModel = new LoginModel(model);
            ModelState.Remove("ConfirmPassword");

            if (!ModelState.IsValid)
                return this.ReturnDueToError(HttpContext, toController: "Account");

            var loginResponse = await _identityService.SignIn(loginModel);
            if (loginResponse.Value == null)
                return this.ReturnDueToError(HttpContext, toController: "Account", errorMessage: loginResponse.ErrorMessage);

            await HttpContext.SetSessionUserData(new SessionUser(loginResponse.Value), _validator);

            HttpContext.Response.Cookies.Append("UserJwtToken", loginResponse.Value.Token,
            new CookieOptions { MaxAge = TimeSpan.FromMinutes(60) });

            ModelState.Clear();
            HttpContext.SetModalMessage("You have successfully passed authorization", ModalMsgType.SuccessMessage);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(AuthRequestModel model)
        {
            var registerModel = new RegisterModel(model);

            if (!ModelState.IsValid)
                return this.ReturnDueToError(HttpContext, toController: "Account", isAuth: false);

            var registerResponse = await _identityService.SignUp(registerModel);
            if (registerResponse.ErrorMessage != null)
                return this.ReturnDueToError(HttpContext, toController: "Account", errorMessage: registerResponse.ErrorMessage, isAuth: false);

            //await HttpContext.SetSessionUserData(new SessionUser(registerResponse.Value), _validator);

            ModelState.Clear();
            HttpContext.SetModalMessage("You have registered a new account. Please sign in using your new credentials.", ModalMsgType.JustMessage);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UpdateUserInfo(UserUpdateModel model, IFormFile image)
        {
            var user = HttpContext.Session.Get<SessionUser>("SessionUser");
            if (await HttpContext.IsValidSessionUser(_validator) == false)
                return this.ReturnDueToError(HttpContext, toController: "Account");

            var imagePath = string.Empty;
            ModelState.Remove("Image");

            if (!ModelState.IsValid)
                return this.ReturnDueToError(HttpContext, toView: nameof(ChangeUserInfo), toController: "Account");

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

            var updateResponse = await _identityService.UpdateUserInfo(userModel);

            if (updateResponse.Value == false)
                HttpContext.SetModalMessage(updateResponse.ErrorMessage, ModalMsgType.ErrorMessage);

            user.UpdateUserInfo(model, imagePath);
            await HttpContext.SetSessionUserData(user, _validator);
            HttpContext.SetModalMessage("User Name, Surname and Avatar was successfully updated!", ModalMsgType.SuccessMessage);
            return RedirectToAction(nameof(Settings));
        }

        public async Task<IActionResult> UpdateUserPassword(PasswordUpdateModel model)
        {
            var user = HttpContext.Session.Get<SessionUser>("SessionUser");
            if (await HttpContext.IsValidSessionUser(_validator) == false )
                return this.ReturnDueToError(HttpContext, toController: "Account");

            if (!ModelState.IsValid)
                return this.ReturnDueToError(HttpContext, toView: nameof(ChangePasswordInfo), toController: "Account", isAuth: false);

            var loginModel = new LoginModel() { Username = user.Username, Password = model.OldPassword };
            var loginResponse = await _identityService.SignIn(loginModel);

            if (loginResponse.Value == null)
                return this.ReturnDueToError(HttpContext, toView: nameof(ChangePasswordInfo), toController: "Account", errorMessage: loginResponse.ErrorMessage, isAuth: false);

            var request = new IdentityUserModel()
            {
                Id = model.UserId,
                Username = user.Username,
                Password = model.NewPassword,
                Token = user.Token
            };

            var updateResponse = await _identityService.UpdatePassword(request);
            if (updateResponse.Value == null)
                return this.ReturnDueToError(HttpContext, toView: nameof(ChangePasswordInfo), toController: "Account", errorMessage: updateResponse.ErrorMessage, isAuth: false);

            HttpContext.SetModalMessage("Password was successfully updated!", ModalMsgType.SuccessMessage);
            return RedirectToAction(nameof(Settings));
        }
    }
}
