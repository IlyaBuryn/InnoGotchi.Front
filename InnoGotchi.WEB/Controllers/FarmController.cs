using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;
using InnoGotchi.WEB.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InnoGotchi.WEB.Controllers
{
    public class FarmController : Controller
    {
        private readonly IFarmService _farmService;
        private readonly IPetService _petService;
        private readonly ICollabService _collabService;
        private readonly IFeedService _feedService;

        public FarmController(IFarmService farmService, IPetService petService,
            ICollabService collabService, IFeedService feedService)
        {
            _farmService = farmService;
            _petService = petService;
            _collabService = collabService;
            _feedService = feedService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.Get<int>("UserId");
            var result = await _farmService.GetFarmByUserId(userId);

            if (result.Value != null)
                HttpContext.Session.Set<int>("FarmId", result.Value.Id);
            return View(result.Value);
        }

        public async Task<IActionResult> Details()
        {
            var userId = HttpContext.Session.Get<int>("UserId");
            var farmResult = await _farmService.GetFarmByUserId(userId);
            await FeedingPeriodStat();
            if (farmResult != null)
            {
                var petsResult = await _petService.GetPetsByFarmId(farmResult.Value.Id);
                farmResult.Value.Pets = petsResult.Value;
            }
            return View(farmResult.Value);
        }

        public async Task<IActionResult> ReadOnlyDetails(int id)
        {
            var farmResult = await _farmService.GetFarmByFarmId((int)id);
            if (farmResult != null)
            {
                var petsResult = await _petService.GetPetsByFarmId(farmResult.Value.Id);
                farmResult.Value.Pets = petsResult.Value;
            }
            return View(farmResult.Value);
        }

        [HttpPost]
        public async Task<JsonResult> InviteFriend(string username)
        {
            var userResponse = await _collabService.GetUserData(username);
            if (userResponse.Value == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(userResponse.ErrorMessage);
            }

            var farmId = HttpContext.Session.Get<int>("FarmId");
            var collabResponse = await _collabService.CreateCollab(
                new CollaboratorModel() { IdentityUserId = userResponse.Value.Id, FarmId = farmId });

            if (collabResponse.Value == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(userResponse.ErrorMessage);
            }

            HttpContext.SetModalMessage($"You successfully add new friend: {username}", ModalMsgType.SuccessMessage);
            return Json($"You successfully add new friend: {username}");
        }

        [HttpPost]
        public async Task<IActionResult> Create(FarmModel model)
        {
            var response = await _farmService.CreateFarm(model);
            if (response.Value == null)
            {
                HttpContext.SetModalMessage(response.ErrorMessage, ModalMsgType.ErrorMessage);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task FeedingPeriodStat()
        {
            var farmId = HttpContext.Session.Get<int>("FarmId");
            var foodPeriod = await _feedService.FeedingFoodPeriod(farmId);
            var drinkPeriod = await _feedService.FeedingDrinkPeriod(farmId);
            HttpContext.Session.Set<double>("AvgFeedingPeriod", foodPeriod.Value ?? 0.0);
            HttpContext.Session.Set<double>("AvgThirstQuenchingPeriod", drinkPeriod.Value ?? 0.0);

        }
    }
}
