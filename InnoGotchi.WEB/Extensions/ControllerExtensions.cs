using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.WEB.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ReturnDueToError(this Controller controller, HttpContext httpContext, bool isAuth = true,
            string toView = "Index", string toController = "Home", string errorMessage = "Validation error!")
        {
            httpContext.SetModalMessage(errorMessage, ModalMsgType.ErrorMessage);
            return controller.RedirectToAction(toView, toController, new { isAuth });
        }


    }
}
