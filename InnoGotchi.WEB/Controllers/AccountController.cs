using InnoGotchi.BusinessLogic.BusinessModels;
using InnoGotchi.BusinessLogic.Extensions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.BusinessLogic.SessionEntities;
using InnoGotchi.WEB.ActionFilters;
using InnoGotchi.WEB.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IWebHostEnvironment _webEnv;

        public AccountController(IWebHostEnvironment webEnv, IAccountService accountService)
        {
            _accountService = accountService;
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


        [TypeFilter(typeof(UserValidationFilter), Arguments = new object[] { true, "Index", "Account" })]
        public IActionResult Settings()
        {
            var user = HttpContext.GetUserFromSession();
            return View(user);
        }


        [TypeFilter(typeof(UserValidationFilter))]
        public IActionResult ChangeUserInfo()
        {
            var user = HttpContext.GetUserFromSession();
            return View(new UserUpdateModel(user));
        }


        [TypeFilter(typeof(UserValidationFilter))]
        public IActionResult ChangePasswordInfo()
        {
            var user = HttpContext.GetUserFromSession();
            return View(new PasswordUpdateModel(user));
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(AuthRequestModel model)
        {
            var result = await _accountService.SignIn(model);
            if (result.ItHasErrors())
                return this.ReturnDueToError(HttpContext, toController: "Account", errorMessage: result.Errors[0]);

            await HttpContext.SetSessionUserData(new SessionUser(result.Value));

            HttpContext.Response.Cookies.Append("UserJwtToken", result.Value.Token,
                new CookieOptions { MaxAge = TimeSpan.FromMinutes(60) });

            HttpContext.SetModalMessage("You have successfully passed authorization", ModalMsgType.SuccessMessage);
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(AuthRequestModel model)
        {
            var result = await _accountService.SignUp(model);
            if (result.ItHasErrors())
                return this.ReturnDueToError(HttpContext, toController: "Account", isAuth: false, errorMessage: result.Errors[0]);

            HttpContext.SetModalMessage("You have registered a new account. Please sign in using your new credentials.", ModalMsgType.JustMessage);
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [TypeFilter(typeof(UserValidationFilter), Arguments = new object[] { true, "Index", "Account" })]
        public async Task<IActionResult> UpdateUserInfo(UserUpdateModel model, IFormFile image)
        {
            var user = HttpContext.GetUserFromSession();

            var result = await _accountService.ChangeUserInfo(model, image);
            if (result.ItHasErrors())
                return this.ReturnDueToError(HttpContext, toView: nameof(ChangeUserInfo), toController: "Account", errorMessage: result.Errors[0]);

            await HttpContext.SetSessionUserData(user);

            HttpContext.SetModalMessage("User Name, Surname and Avatar was successfully updated! ", ModalMsgType.SuccessMessage);
            return RedirectToAction(nameof(Settings));
        }


        [HttpPost]
        [TypeFilter(typeof(UserValidationFilter), Arguments = new object[] { true, "Index", "Account" })]
        public async Task<IActionResult> UpdateUserPassword(PasswordUpdateModel model)
        {
            var result = await _accountService.ChangePassword(model);
            if (result.ItHasErrors())
                return this.ReturnDueToError(HttpContext, toView: nameof(ChangePasswordInfo),
                    toController: "Account", isAuth: false,  errorMessage: result.Errors[0]);

            HttpContext.SetModalMessage("Password was successfully updated!", ModalMsgType.SuccessMessage);
            return RedirectToAction(nameof(Settings));
        }
    }
}
