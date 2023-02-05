using FluentValidation;
using InnoGotchi.BusinessLogic.Extensions;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.BusinessLogic.SessionEntities;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Interfaces.HttpClients;
using InnoGotchi.DataAccess.Models.ResponseModels;
using InnoGotchi.DataAccess.Extensions;
using InnoGotchi.WEB.Extensions;
using Microsoft.AspNetCore.Http;

namespace InnoGotchi.BusinessLogic.Services
{
    public class FarmService : IFarmService
    {
        private readonly IFarmClient _farmClient;
        private readonly IPetClient _petClient;
        private readonly ICollabClient _collabClient;
        private readonly IFeedClient _feedClient;
        private readonly IValidator<SessionUser> _sessionUserValidator;
        private readonly IValidator<SessionFarm> _sessionFarmValidator;
        private readonly IValidator<FarmDto> _farmValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public FarmService(IFarmClient farmClient,
            IPetClient petClient,
            ICollabClient collabClient,
            IFeedClient feedClient, 
            IValidator<SessionUser> sessionUserValidator, 
            IValidator<SessionFarm> sessionFarmValidator,
            IValidator<FarmDto> farmValidator,
            IHttpContextAccessor httpContextAccessor)
        {
            _farmClient = farmClient;
            _petClient = petClient;
            _collabClient = collabClient;
            _feedClient = feedClient;
            _sessionUserValidator = sessionUserValidator;
            _sessionFarmValidator = sessionFarmValidator;
            _farmValidator = farmValidator;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<ResponseModel<FarmDto>> CheckFarmDetails(int farmId)
        {
            var result = new ResponseModel<FarmDto>();

            var farmResult = await _farmClient.GetFarmByFarmId(farmId);
            var foodPeriod = await _feedClient.FeedingFoodPeriod(farmId);
            var drinkPeriod = await _feedClient.FeedingDrinkPeriod(farmId);
            var petsResult = await _petClient.GetPetsByFarmId(farmId);

            if (farmResult.ItHasErrorsOrValueIsNull())
            {
                return result.SetAndReturnError(farmResult.Error);
            }

            if (petsResult.ItHasErrorsOrValueIsNull())
            {
                return result.SetAndReturnError(petsResult.Error);
            }

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return result.SetAndReturnError(result.HttpContextError(string.Empty));
            }
            httpContext.Session.Set<double>("AvgFeedingPeriod", foodPeriod.Value ?? 0.0);
            httpContext.Session.Set<double>("AvgThirstQuenchingPeriod", drinkPeriod.Value ?? 0.0);

            if (farmResult.Value == null)
            {
                return result.SetAndReturnError(farmResult.ValueIsNullError());
            }
            farmResult.Value.Pets = petsResult.Value;

            result.Value = farmResult.Value;
            return result;
        }


        public async Task<ResponseModel<FarmDto>> CheckFarmReadOnlyDetails(int farmId)
        {
            var result = new ResponseModel<FarmDto>();

            var farmResult = await _farmClient.GetFarmByFarmId(farmId);
            if (farmResult.ItHasErrorsOrValueIsNull())
            {
                return result.SetAndReturnError(farmResult.Error);
            }

            if (farmResult.Value == null)
            {
                return result.SetAndReturnError(farmResult.ValueIsNullError());
            }
            var petsResult = await _petClient.GetPetsByFarmId(farmResult.Value.Id);

            if (petsResult.ItHasErrorsOrValueIsNull())
            {
                return result.SetAndReturnError(petsResult.Error);
            }

            farmResult.Value.Pets = petsResult.Value;
            result.Value = farmResult.Value;
            return result;
        }


        public async Task<ResponseModel<int?>> CreateFarm(FarmDto farmDto)
        {
            var result = new ResponseModel<int?>();
            var validationResult = await _farmValidator.ValidateAsync(farmDto);
            if (!validationResult.IsValidResult(result))
            {
                return result;
            }

            var response = await _farmClient.CreateFarm(farmDto);
            response.ItHasErrorsOrValueIsNull();
            return response;
        }


        public async Task<ResponseModel<FarmDto>> GetUserFarm(int userId)
        {
            var result = new ResponseModel<FarmDto>();

            var farmResult = await _farmClient.GetFarmByUserId(userId);
            if (farmResult.ItHasErrorsOrValueIsNull())
            {
                return result.SetAndReturnError(farmResult.Error);
            }

            if (farmResult.Value != null)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null)
                {
                    return result.SetAndReturnError(result.HttpContextError(string.Empty));
                }
                httpContext.SetSessionFarmData(new SessionFarm(farmResult.Value.Id));
            }

            result.Value = farmResult.Value;
            return result;
        }


        public async Task<ResponseModel<CollaboratorDto>> InviteFriend(string username)
        {
            var result = new ResponseModel<CollaboratorDto>();

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return result.SetAndReturnError(result.HttpContextError(string.Empty));
            }
            var farm = httpContext.GetFarmFromSession();

            var userResponse = await _collabClient.GetUserData(username);
            if (userResponse.ItHasErrorsOrValueIsNull())
            {
                return result.SetAndReturnError(userResponse.Error);
            }

            if (userResponse.Value == null)
            {
                return result.SetAndReturnError(userResponse.ValueIsNullError());
            }

            var newCollab = new CollaboratorDto() { IdentityUserId = userResponse.Value.Id, FarmId = farm.Id };

            var collabResponse = await _collabClient.CreateCollab(newCollab);
            if (collabResponse.ItHasErrorsOrValueIsNull())
            {
                return result.SetAndReturnError(collabResponse.Error);
            }

            result.Value = newCollab;
            return result;
        }
    }
}
