using InnoGotchi.BusinessLogic.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using InnoGotchi.WEB.Extensions;

namespace InnoGotchi.WEB.ActionFilters
{
    public class UserValidationFilter : IAsyncActionFilter
    {
        private readonly bool _isAuthorization;
        private readonly string _toView;
        private readonly string _toController;


        public UserValidationFilter(bool isAuthorization = true, 
            string toView = "Index", 
            string toController = "Home")
        {
            _isAuthorization = isAuthorization;
            _toView = toView;
            _toController = toController;
        }


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.GetUserFromSession();
            if (user == null)
            {
                var controller = (Controller)context.Controller;
                context.Result = controller.ReturnDueToError(context.HttpContext,
                    toController: _toController,
                    toView: _toView,
                    isAuth: _isAuthorization,
                    errorMessage: "User validation error!");

                return;
            }

            var result = await next();
        }
    }
}
