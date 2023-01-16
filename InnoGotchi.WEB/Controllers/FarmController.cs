using InnoGotchi.BusinessLogic.Extensions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.BusinessLogic.SessionEntities;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.WEB.ActionFilters;
using InnoGotchi.WEB.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InnoGotchi.WEB.Controllers
{
    public class FarmController : Controller
    {
        private readonly IFarmService _farmService;


        public FarmController(IFarmService farmService)
        {
            _farmService = farmService;
        }


        [TypeFilter(typeof(UserValidationFilter))]
        public async Task<IActionResult> Index()
        {
            var user = HttpContext.GetUserFromSession();

            var result = await _farmService.GetUserFarm(user.Id);

            if (result.Value != null)
                await HttpContext.SetSessionFarmData(new SessionFarm(result.Value.Id));

            return View(result.Value);
        }


        [TypeFilter(typeof(FarmValidationFilter))]
        public async Task<IActionResult> Details()
        { 
            var farm = HttpContext.GetFarmFromSession();

            var farmDetails = await _farmService.CheckFarmDetails(farm.Id);
            if (farmDetails.ItHasErrors())
                return this.ReturnDueToError(HttpContext, errorMessage: farmDetails.Errors[0]);

            return View(farmDetails.Value);
        }


        public async Task<IActionResult> ReadOnlyDetails(int id)
        {
            var farmDetails = await _farmService.CheckFarmReadOnlyDetails(id);
            if (farmDetails.ItHasErrors())
                return this.ReturnDueToError(HttpContext, errorMessage: farmDetails.Errors[0]);

            return View(farmDetails.Value);
        }


        [HttpPost]
        [TypeFilter(typeof(FarmValidationFilter))]
        public async Task<JsonResult> InviteFriend(string username)
        {
            var farm = HttpContext.GetFarmFromSession();

            var inviteResponse = await _farmService.InviteFriend(username);
            if (inviteResponse.ItHasErrors())
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(inviteResponse.Error);
            }

            HttpContext.SetModalMessage($"You successfully add new friend: {username}", ModalMsgType.SuccessMessage);
            return Json($"You successfully add new friend: {username}");
        }


        [HttpPost]
        public async Task<IActionResult> Create(FarmDto model)
        {
            var response = await _farmService.CreateFarm(model);
            if (response.ItHasErrors())
                return this.ReturnDueToError(HttpContext, errorMessage: response.Errors[0]);

            return RedirectToAction(nameof(Index));
        }
    }
}
