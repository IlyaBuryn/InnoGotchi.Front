using InnoGotchi.Http.Interfaces;
using InnoGotchi.WEB.Utility;
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

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.Get<int>("UserId");
            var farms = await _collabService.GetCollabsByUser(userId);
            return View(farms.Value);
        }
    }
}
