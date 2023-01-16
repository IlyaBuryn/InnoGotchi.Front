using InnoGotchi.BusinessLogic.Extensions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.WEB.ActionFilters;
using InnoGotchi.WEB.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.WEB.Controllers
{
    public class CollabController : Controller
    {
        private readonly ICollabService _collabService;


        public CollabController(ICollabService collabService)
        {
            _collabService = collabService;
        }


        [TypeFilter(typeof(UserValidationFilter))]
        public async Task<IActionResult> Index()
        {
            var user = HttpContext.GetUserFromSession();

            var result = await _collabService.GetUserCollabs(user.Id);
            if (result.ItHasErrors())
                return this.ReturnDueToError(HttpContext, toController: "Collab", errorMessage: result.Errors[0]);
            
            return View(result.Value);
        }
    }
}
