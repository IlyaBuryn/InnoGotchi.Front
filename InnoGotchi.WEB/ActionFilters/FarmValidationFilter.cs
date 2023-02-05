using InnoGotchi.BusinessLogic.Extensions;
using InnoGotchi.WEB.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InnoGotchi.WEB.ActionFilters
{
    public class FarmValidationFilter : IAsyncActionFilter
    {
        private readonly bool _isAuthorization;
        private readonly string _toView;
        private readonly string _toController;


        public FarmValidationFilter(bool isAuthorization = true, 
            string toView = "Index", 
            string toController = "Home")
        {
            _isAuthorization = isAuthorization;
            _toView = toView;
            _toController = toController;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var farm = context.HttpContext.GetFarmFromSession();
            if (farm == null)
            {
                var controller = (Controller)context.Controller;
                context.Result = controller.ReturnDueToError(context.HttpContext,
                    toController: _toController,
                    toView: _toView,
                    isAuth: _isAuthorization,
                    errorMessage: "Farm validation error!");

                return;
            }


            var result = await next();
        }
    }
}
