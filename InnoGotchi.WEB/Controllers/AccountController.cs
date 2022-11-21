using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;
using InnoGotchi.WEB.Models;
using InnoGotchi.WEB.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;

namespace InnoGotchi.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly IWebHostEnvironment _webEnv;
        public AccountController(IIdentityService identityService,
            IWebHostEnvironment webEnv,
            IFarmService farmService)
        {
            _identityService = identityService;
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

        public async Task<IActionResult> Settings()
        {
            HttpContext.SetUserModel(out IdentityUserModel userModel);
            if (userModel.Id != null && userModel.Id != 0)
                return View(userModel);
            else
                return RedirectToAction(nameof(Index), new { isAuth = true });
        }

        public IActionResult ChangeUserInfo()
        {
            HttpContext.SetUserModel(out IdentityUserModel userModel);
            if (userModel.Id != 0)
                return View(new UserUpdateModel()
                {
                    UserId = userModel.Id,
                    Name = userModel.Name,
                    Surname = userModel.Surname,
                    Image = userModel.Image
                });
            else
                return RedirectToAction("Index", "Home");
        }

        public IActionResult ChangePasswordInfo()
        {
            HttpContext.SetUserModel(out IdentityUserModel userModel);
            if (userModel.Id != 0)
                return View(new PasswordUpdateModel()
                {
                    UserId = userModel.Id,
                });
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(AuthRequestModel model)
        {
            var request = new LoginModel(model);

            ModelState.Remove("ConfirmPassword");

            if (ModelState.IsValid)
            {
                var result = await _identityService.SignIn(request);
                if (result.ErrorMessage == null)
                {
                    HttpContext.KeepUserInData(result.Value);
                    HttpContext.Response.Cookies.Append("UserJwtToken", result.Value.Token,
                    new CookieOptions
                    {
                        MaxAge = TimeSpan.FromMinutes(60)
                    });

                    HttpContext.SetModalMessage("You have successfully passed authorization", ModalMsgType.SuccessMessage);
                    return RedirectToAction("Index", "Home");
                }
                HttpContext.SetModalMessage(result.ErrorMessage, ModalMsgType.ErrorMessage);
                ModelState.Clear();
            }
            return RedirectToAction(nameof(Index), new { isAuth = true });
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(AuthRequestModel model)
        {
            var request = new RegisterModel(model);

            if (ModelState.IsValid)
            {
                var result = await _identityService.SignUp(request);

                if (result.ErrorMessage == null)
                {
                    HttpContext.SetModalMessage("You have registered a new account. Please sign in using your new credentials.", ModalMsgType.JustMessage);
                    return RedirectToAction("Index", "Home");
                }
                HttpContext.SetModalMessage(result.ErrorMessage, ModalMsgType.ErrorMessage);
                ModelState.Clear();
            }
            return RedirectToAction(nameof(Index), new { isAuth = false });
        }

        public async Task<IActionResult> UpdateUserInfo(UserUpdateModel model, IFormFile image)
        {
            if (model.UserId == 0)
                return RedirectToAction("Index", "Home");

            var imagePath = string.Empty;

            ModelState.Remove("Image");

            if (!ModelState.IsValid)
            {
                HttpContext.SetModalMessage("Model is invalid!", ModalMsgType.ErrorMessage);
                ModelState.Clear();
                return RedirectToAction(nameof(Settings));

            }

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
                imagePath = HttpContext.Session.Get<string>("ImagePath");
            }

            var request = new IdentityUserModel()
            {
                Id = model.UserId,
                Username = HttpContext.Session.Get<string>("Username"),
                Token = HttpContext.Session.Get<string>("UserToken"),
                Name = model.Name,
                Surname = model.Surname,
                Image = imagePath,
            };
            var result = await _identityService.UpdateUserInfo(request);

            if (result.ErrorMessage == null && result.Value != false)
            {
                HttpContext.Session.Set<string>("Name", model.Name);
                HttpContext.Session.Set<string>("Surname", model.Surname);
                HttpContext.Session.Set<string>("ImagePath", imagePath);

                HttpContext.SetModalMessage("User Name, Surname and Avatar was successfully updated!", ModalMsgType.SuccessMessage);
                return RedirectToAction(nameof(Settings));
            }

            HttpContext.SetModalMessage(result.ErrorMessage, ModalMsgType.ErrorMessage);
            ModelState.Clear();
            return RedirectToAction(nameof(Settings));

        }

        public async Task<IActionResult> UpdateUserPassword(PasswordUpdateModel model)
        {
            if (model.UserId == 0)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                HttpContext.SetModalMessage("Model is invalid!", ModalMsgType.ErrorMessage);
                return RedirectToAction(nameof(Index), new { isAuth = false });
            }

            var username = HttpContext.Session.Get<string>("Username");

            if (string.IsNullOrEmpty(username))
            {
                HttpContext.SetModalMessage("Username is null", ModalMsgType.ErrorMessage);
                return RedirectToAction("Index", "Home");
            }

            var requestUser = new LoginModel() { Username = username, Password = model.OldPassword };
            var result = await _identityService.SignIn(requestUser);

            if (result.ErrorMessage == null)
            {
                var request = new IdentityUserModel()
                {
                    Id = model.UserId,
                    Username = username,
                    Password = model.NewPassword,
                    Token = HttpContext.Session.Get<string>("UserToken")
                };

                var isUpdated = await _identityService.UpdatePassword(request);
                if (isUpdated.ErrorMessage == null)
                {
                    HttpContext.SetModalMessage("Password was successfully updated!", ModalMsgType.SuccessMessage);
                    return RedirectToAction(nameof(Settings));
                }
            }

            HttpContext.SetModalMessage(result.ErrorMessage, ModalMsgType.ErrorMessage);
            return RedirectToAction(nameof(ChangePasswordInfo));
        }

    }
}
