using FluentValidation;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;
using InnoGotchi.Components.Settings;
using InnoGotchi.Http.Interfaces;
using InnoGotchi.WEB.Models;
using InnoGotchi.WEB.Utility;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.WEB.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetService _petService;
        private readonly IBodyPartService _bodyPartService;
        private readonly ICollabService _collabService;
        private readonly IFeedService _feedService;
        private readonly IValidator<SessionUser> _userValidator;
        private readonly IValidator<SessionFarm> _farmValidator;

        public PetController(IPetService petService, IBodyPartService bodyPartService, IFeedService feedService,
            IValidator<SessionUser> userValidator, IValidator<SessionFarm> farmValidator)
        {
            _petService = petService;
            _bodyPartService = bodyPartService;
            _feedService = feedService;
            _userValidator = userValidator;
            _farmValidator = farmValidator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var parts = await _bodyPartService.GetBodyParts();
            ViewData["Bodies"] = parts.Value.Where(x => x.BodyPartTypeId == 1).ToList();
            ViewData["Noses"] = parts.Value.Where(x => x.BodyPartTypeId == 2).ToList();
            ViewData["Mouths"] = parts.Value.Where(x => x.BodyPartTypeId == 3).ToList();
            ViewData["Eyes"] = parts.Value.Where(x => x.BodyPartTypeId == 4).ToList();
            return View();
        }

        public async Task<IActionResult> Details(int pet, bool isReadOnly)
        {
            var petResponse = await _petService.GetPetById(pet);
            if (petResponse.Value == null)
                return this.ReturnDueToError(HttpContext, errorMessage: petResponse.ErrorMessage);

            return View(new PetViewModel(petResponse.Value, isReadOnly));
        }

        public async Task<IActionResult> AllPetsPage(int page, SortFilter sortType = SortFilter.ByHappinessDays)
        {
            HttpContext.Session.Set<SortFilter>("CurrentFilter", sortType);

            var countResponse = await _petService.GetAllPetsCount();
            if (countResponse.Value == null)
                return this.ReturnDueToError(HttpContext, errorMessage: countResponse.ErrorMessage);

            var pageResponse = await _petService.GetAllPets(page, sortType);
            if (pageResponse.Value == null)
                return this.ReturnDueToError(HttpContext, errorMessage: pageResponse.ErrorMessage);

            var pageViewModel = new PageViewModel((int)countResponse.Value, page, DefaultSettings.PageSize);
            var petsModel = new PageIndexViewModel<PetDto> { 
                PageViewModel = pageViewModel,
                Items = pageResponse.Value
            };

            return View(petsModel);
        }

        public async Task<IActionResult> FeedPet(int pet)
        {
            var user = HttpContext.Session.Get<SessionUser>("SessionUser");
            if (await HttpContext.IsValidSessionUser(_userValidator) == false)
                return this.ReturnDueToError(HttpContext, errorMessage: "User validation error!");

            var request = new FeedDto(pet, user.Id);

            var response = await _feedService.FeedPet(request);
            if (response.Value == null)
            {
                HttpContext.SetModalMessage(response.ErrorMessage, ModalMsgType.ErrorMessage);
                return RedirectToAction(nameof(Details), new { pet });
            }

            HttpContext.SetModalMessage("", ModalMsgType.SuccessMessage);
            return RedirectToAction(nameof(Details), new { pet });
        }

        public async Task<IActionResult> DrinkPet(int pet)
        {
            var user = HttpContext.Session.Get<SessionUser>("SessionUser");
            if (await HttpContext.IsValidSessionUser(_userValidator) == false)
                return this.ReturnDueToError(HttpContext, errorMessage: "User vallidation error!");

            var request = new FeedDto(pet, user.Id);

            var response = await _feedService.DrinkPet(request);
            if (response.Value == null)
            {
                HttpContext.SetModalMessage(response.ErrorMessage, ModalMsgType.ErrorMessage);
                return RedirectToAction(nameof(Details), new { pet });
            }
            else
            {
                HttpContext.SetModalMessage("", ModalMsgType.SuccessMessage);
                return RedirectToAction(nameof(Details), new { pet });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(PetDto model)
        {
            var farm = HttpContext.Session.Get<SessionFarm>("SessionFarm");
            if (await HttpContext.IsValidSessionFarm(_farmValidator) == false)
                return this.ReturnDueToError(HttpContext, errorMessage: "Farm vallidation error!");

            model.FarmId = farm.Id;

            ModelState.Remove("VitalSign");
            ModelState.Remove("Farm");

            if (!ModelState.IsValid)
                return this.ReturnDueToError(HttpContext, toView: nameof(Create), toController: "Pet");

            model.VitalSign = new VitalSignDto();
            var response = await _petService.CreatePet(model);
            if (response.Value == null)
                return this.ReturnDueToError(HttpContext, toView: nameof(Create), toController: "Pet", errorMessage: response.ErrorMessage);

            HttpContext.SetModalMessage("Pet was successfully created!", ModalMsgType.SuccessMessage);
            return RedirectToAction("Details", "Farm");
        }
    }
}
