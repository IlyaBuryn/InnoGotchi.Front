using FluentValidation;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Http.Interfaces;
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
        private readonly IValidator<SessionUser> _userValidator;
        private readonly IValidator<SessionFarm> _farmValidator;

        public FarmController(IFarmService farmService, IPetService petService,
            ICollabService collabService, IFeedService feedService,
            IValidator<SessionUser> userValidator,
            IValidator<SessionFarm> farmValidator)
        {
            _farmService = farmService;
            _petService = petService;
            _collabService = collabService;
            _feedService = feedService;
            _userValidator = userValidator;
            _farmValidator = farmValidator;
        }

        public async Task<IActionResult> Index()
        {
            var user = HttpContext.Session.Get<SessionUser>("SessionUser");
            if (await HttpContext.IsValidSessionUser(_userValidator) == false)
                return this.ReturnDueToError(HttpContext, errorMessage: "User vallidation error!");

            var result = await _farmService.GetFarmByUserId(user.Id);
            if (result.Value != null)
                await HttpContext.SetSessionFarmData(new SessionFarm(result.Value.Id), _farmValidator);

            return View(result.Value);
        }

        public async Task<IActionResult> Details()
        { 
            var farm = HttpContext.Session.Get<SessionFarm>("SessionFarm");
            if (await HttpContext.IsValidSessionFarm(_farmValidator) == false)
                return this.ReturnDueToError(HttpContext, errorMessage: "Farm vallidation error!");

            var farmResult = await _farmService.GetFarmByFarmId(farm.Id);
            if (farmResult.Value == null)
                return this.ReturnDueToError(HttpContext, errorMessage: farmResult.ErrorMessage);

            var foodPeriod = await _feedService.FeedingFoodPeriod(farm.Id);
            var drinkPeriod = await _feedService.FeedingDrinkPeriod(farm.Id);
            HttpContext.Session.Set<double>("AvgFeedingPeriod", foodPeriod.Value ?? 0.0);
            HttpContext.Session.Set<double>("AvgThirstQuenchingPeriod", drinkPeriod.Value ?? 0.0);

            var petsResult = await _petService.GetPetsByFarmId(farm.Id);
            farmResult.Value.Pets = petsResult.Value;
            return View(farmResult.Value);
        }

        public async Task<IActionResult> ReadOnlyDetails(int id)
        {
            var farmResult = await _farmService.GetFarmByFarmId(id);
            if (farmResult.Value == null)
                return this.ReturnDueToError(HttpContext, errorMessage: farmResult.ErrorMessage);

            var petsResult = await _petService.GetPetsByFarmId(farmResult.Value.Id);
            farmResult.Value.Pets = petsResult.Value;
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

            var farm = HttpContext.Session.Get<SessionFarm>("SessionFarm");
            var collabResponse = await _collabService.CreateCollab(
                new CollaboratorDto() { IdentityUserId = userResponse.Value.Id, FarmId = farm.Id });

            if (collabResponse.Value == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(userResponse.ErrorMessage);
            }

            HttpContext.SetModalMessage($"You successfully add new friend: {username}", ModalMsgType.SuccessMessage);
            return Json($"You successfully add new friend: {username}");
        }

        [HttpPost]
        public async Task<IActionResult> Create(FarmDto model)
        {
            var response = await _farmService.CreateFarm(model);
            if (response.Value == null)
                return this.ReturnDueToError(HttpContext, errorMessage: response.ErrorMessage);

            return RedirectToAction(nameof(Index));
        }
    }
}
