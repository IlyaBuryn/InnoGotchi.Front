using InnoGotchi.Http.Components.Enums;
using InnoGotchi.Http.Interfaces;
using InnoGotchi.Http.Models;
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

        public PetController(IPetService petService, IBodyPartService bodyPartService, IFeedService feedService)
        {
            _petService = petService;
            _bodyPartService = bodyPartService;
            _feedService = feedService;
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
            var petRequest = await _petService.GetPetById((int)pet);
            if (petRequest.Value == null)
            {
                HttpContext.SetModalMessage(petRequest.ErrorMessage, ModalMsgType.ErrorMessage);
                return RedirectToAction("Index", "Home");
            }
            return View(new PetViewModel(petRequest.Value, isReadOnly));
        }

        public async Task<IActionResult> AllPetsPage(int page, SortFilter sortType = SortFilter.ByHappinessDays)
        {
            HttpContext.Session.Set<SortFilter>("CurrentFilter", sortType);

            var countResponse = await _petService.GetAllPetsCount();
            if (countResponse.Value == null)
            {
                HttpContext.SetModalMessage(countResponse.ErrorMessage, ModalMsgType.ErrorMessage);
                return RedirectToAction("Index", "Home");
            }

            var pageResponse = await _petService.GetAllPets(page, sortType);
            if (pageResponse.Value == null)
            {
                HttpContext.SetModalMessage(pageResponse.ErrorMessage, ModalMsgType.ErrorMessage);
                return RedirectToAction("Index", "Home");
            }

            var pageViewModel = new PageViewModel((int)countResponse.Value, page, 15); // TODO: 
            var petsModel = new PageIndexViewModel<PetModel> { 
                PageViewModel = pageViewModel,
                Items = pageResponse.Value
            };

            return View(petsModel);
        }

        public async Task<IActionResult> FeedPet(int pet)
        {
            var userId = HttpContext.Session.Get<int>("UserId");
            var request = new FeedModel()
            {
                IdentityUserId = userId,
                FeedTime = DateTime.Now,
                FoodCount = 1,
                WaterCount = 0,
                PetId = pet
            };
            var response = await _feedService.FeedPet(request);
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

        public async Task<IActionResult> DrinkPet(int pet)
        {
            var userId = HttpContext.Session.Get<int>("UserId");
            var request = new FeedModel()
            {
                IdentityUserId = userId,
                FeedTime = DateTime.Now,
                FoodCount = 0,
                WaterCount = 1,
                PetId = pet
            };
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
        public async Task<IActionResult> Create(PetModel model)
        {
            var farmId = HttpContext.Session.Get<int>("FarmId");
            model.FarmId = farmId;

            ModelState.Remove("VitalSign");
            ModelState.Remove("Farm");

            if (!ModelState.IsValid)
            {
                HttpContext.SetModalMessage("Model is not valid", ModalMsgType.ErrorMessage);
                return RedirectToAction(nameof(Create));
            }

            model.VitalSign = new VitalSignModel();
            var response = await _petService.CreatePet(model);
            if (response.Value == null)
            {
                HttpContext.SetModalMessage(response.ErrorMessage, ModalMsgType.ErrorMessage);
                return RedirectToAction(nameof(Create));
            }

            HttpContext.SetModalMessage("Pet was successfully created!", ModalMsgType.SuccessMessage);
            return RedirectToAction("Details", "Farm");
        }


    }
}
