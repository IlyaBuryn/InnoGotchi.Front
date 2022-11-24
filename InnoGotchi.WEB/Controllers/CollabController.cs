using FluentValidation;
using InnoGotchi.Http.Interfaces;
using InnoGotchi.WEB.Utility;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.WEB.Controllers
{
    public class CollabController : Controller
    {
        private readonly ICollabService _collabService;
        private readonly IValidator<SessionUser> _validator;

        public CollabController(ICollabService collabService, IValidator<SessionUser> validator)
        {
            _collabService = collabService;
            _validator = validator;
        }

        public async Task<IActionResult> Index()
        {
            var user = HttpContext.Session.Get<SessionUser>("SessionUser");
            if (await HttpContext.IsValidSessionUser(_validator) == false)
                return this.ReturnDueToError(HttpContext, errorMessage: "User validation error!");

            var farms = await _collabService.GetCollabsByUser(user.Id);
            return View(farms.Value);
        }
    }
}
