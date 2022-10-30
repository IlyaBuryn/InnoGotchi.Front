using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;
using InnoGotchi.WEB.Utility;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;
        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public IActionResult Index(bool isAuth)
        {
            HttpContext.Session.Set("IsAuthorization", isAuth);
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Logout();
            return View();
        }

        public IActionResult LoggedIn()
        {
            return View();
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
                    HttpContext.KeepUserInData(result);
                    return RedirectToAction(nameof(LoggedIn));
                }
                HttpContext.Session.Set("AuthenticateError", result.ErrorMessage);
                ModelState.Clear();
            }
            return RedirectToAction(nameof(Index), true);
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
                    return RedirectToAction(nameof(Index), false);
                }
                HttpContext.Session.Set("AuthenticateError", result.ErrorMessage);
                ModelState.Clear();
            }
            return RedirectToAction(nameof(Index), false);
        }

    }
}
