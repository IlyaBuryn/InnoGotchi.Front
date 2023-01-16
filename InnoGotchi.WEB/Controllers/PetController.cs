using InnoGotchi.BusinessLogic.Extensions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;
using InnoGotchi.Components.Settings;
using InnoGotchi.WEB.ActionFilters;
using InnoGotchi.WEB.Extensions;
using InnoGotchi.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.WEB.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetService _petService;


        public PetController(IPetService petService)
        {
            _petService = petService;
        }


        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Create()
        {
            var bodyPartsResponse = await _petService.BodyPartsForCreatingPetView();
            if (bodyPartsResponse.ItHasErrors())
                return this.ReturnDueToError(HttpContext, errorMessage: bodyPartsResponse.Error);

            int i = 1;
            foreach(var part in bodyPartsResponse.Value)
            {
                ViewData[part.Key] = part.Value.Where(x => x.BodyPartTypeId == i).ToList();
                i++;
            }
            return View();
        }


        public async Task<IActionResult> Details(int pet, bool isReadOnly)
        {
            var petResponse = await _petService.PetDetails(pet);
            if (petResponse.ItHasErrors())
                return this.ReturnDueToError(HttpContext, errorMessage: petResponse.Error);

            return View(new PetViewModel(petResponse.Value, isReadOnly));
        }


        public async Task<IActionResult> AllPetsPage(int page, SortFilter sortType = SortFilter.ByHappinessDays)
        {
            HttpContext.Session.Set<SortFilter>("CurrentFilter", sortType);

            var pageModelResponse = await _petService.PetPage(page, sortType);
            if (pageModelResponse.ItHasErrors())
                return this.ReturnDueToError(HttpContext, errorMessage: pageModelResponse.Error);

            var pageViewModel = new PageViewModel((int)pageModelResponse.Value.TotalPets, page, DefaultSettings.PageSize);
            var petsModel = new PageIndexViewModel<PetDto> { 
                PageViewModel = pageViewModel,
                Items = pageModelResponse.Value.PetsOnPage,
            };

            return View(petsModel);
        }


        [TypeFilter(typeof(UserValidationFilter))]
        public async Task<IActionResult> FeedPet(int pet)
        {
            var user = HttpContext.GetUserFromSession();

            var feedResponse = await _petService.FeedPet(new FeedDto(pet, user.Id));
            if (feedResponse.ItHasErrors())
            {
                HttpContext.SetModalMessage(feedResponse.Error, ModalMsgType.ErrorMessage);
                return RedirectToAction(nameof(Details), new { pet });
            }
            else
            {
                HttpContext.SetModalMessage("", ModalMsgType.SuccessMessage);
                return RedirectToAction(nameof(Details), new { pet });
            }
        }


        [TypeFilter(typeof(UserValidationFilter))]
        public async Task<IActionResult> DrinkPet(int pet)
        {
            var user = HttpContext.GetUserFromSession();

            var feedResponse = await _petService.DrinkPet(new FeedDto(pet, user.Id));
            if (feedResponse.ItHasErrors())
            {
                HttpContext.SetModalMessage(feedResponse.Error, ModalMsgType.ErrorMessage);
                return RedirectToAction(nameof(Details), new { pet });
            }
            else
            {
                HttpContext.SetModalMessage("", ModalMsgType.SuccessMessage);
                return RedirectToAction(nameof(Details), new { pet });
            }
        }


        [HttpPost]
        [TypeFilter(typeof(FarmValidationFilter))]
        public async Task<IActionResult> Create(PetDto model)
        {
            var farm = HttpContext.GetFarmFromSession();
            model.FarmId = farm.Id;

            var response = await _petService.CreatePet(model);
            if (response.ItHasErrors())
                return this.ReturnDueToError(HttpContext, toView: nameof(Create), toController: "Pet", errorMessage: response.Error);

            HttpContext.SetModalMessage($"Pet was successfully created!", ModalMsgType.SuccessMessage);
            return RedirectToAction("Details", "Farm");
        }
    }
}
