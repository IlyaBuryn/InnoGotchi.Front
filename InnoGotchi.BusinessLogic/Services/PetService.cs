using FluentValidation;
using InnoGotchi.BusinessLogic.BusinessModels;
using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.Components.Enums;
using InnoGotchi.DataAccess.Interfaces.HttpClients;
using InnoGotchi.DataAccess.Models.ResponseModels;
using InnoGotchi.WEB.Extensions;

namespace InnoGotchi.BusinessLogic.Services
{
    public class PetService : IPetService
    {
        private readonly IPetClient _petClient;
        private readonly IFeedClient _feedClient;
        private readonly IBodyPartClient _bodyPartClient;
        private readonly IValidator<PetDto> _petValidator;
        private readonly IValidator<FeedDto> _feedValidator;

        public PetService(IPetClient petClient,
            IFeedClient feedClient,
            IBodyPartClient bodyPartClient,
            IValidator<PetDto> petValidator,
            IValidator<FeedDto> feedValidator)
        {
            _petClient = petClient;
            _feedClient = feedClient;
            _bodyPartClient = bodyPartClient;
            _petValidator = petValidator;
            _feedValidator = feedValidator;
        }


        public async Task<ResponseModel<int?>> CreatePet(PetDto pet)
        {
            var result = new ResponseModel<int?>();
            var validationResult = await _petValidator.ValidateAsync(pet);
            if (!validationResult.IsValidResult<int?>(result))
                return result;

            pet.VitalSign = new VitalSignDto();

            var createResponse = await _petClient.CreatePet(pet);
            if (createResponse.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(createResponse.Error);

            result.Value = createResponse.Value;
            return result;
        }


        public async Task<ResponseModel<int?>> DrinkPet(FeedDto feedDto)
        {
            var result = new ResponseModel<int?>();
            var validationResult = await _feedValidator.ValidateAsync(feedDto);
            if (!validationResult.IsValidResult<int?>(result))
                return result;

            var drinkResponse = await _feedClient.DrinkPet(feedDto);
            if (drinkResponse.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(drinkResponse.Error);

            result.Value = drinkResponse.Value;
            return result;
        }


        public async Task<ResponseModel<int?>> FeedPet(FeedDto feedDto)
        {
            var result = new ResponseModel<int?>();
            var validationResult = await _feedValidator.ValidateAsync(feedDto);
            if (!validationResult.IsValidResult<int?>(result))
                return result;

            var feedResponse = await _feedClient.FeedPet(feedDto);
            if (feedResponse.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(feedResponse.Error);

            result.Value = feedResponse.Value;
            return result;
        }


        public async Task<ResponseModel<PageModel>> PetPage(int pageNum, SortFilter sortFilter)
        {
            var result = new ResponseModel<PageModel>();

            var countResponse = await _petClient.GetAllPetsCount();
            if (countResponse.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(countResponse.Error);

            var pageResponse = await _petClient.GetAllPets(pageNum, sortFilter);
            if (pageResponse.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(pageResponse.Error);

            result.Value = new PageModel(pageResponse.Value, countResponse.Value);
            return result;
        }


        public async Task<ResponseModel<PetDto>> PetDetails(int petId)
        {
            var result = new ResponseModel<PetDto>();

            var petResponse = await _petClient.GetPetById(petId);
            if (petResponse.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(petResponse.Error);

            result.Value = petResponse.Value;
            return result;
        }


        public async Task<ResponseModel<Dictionary<string, List<BodyPartDto>>>> BodyPartsForCreatingPetView()
        {
            var result = new ResponseModel<Dictionary<string, List<BodyPartDto>>>();

            var bodyPartsResponse = await _bodyPartClient.GetBodyParts();
            if (bodyPartsResponse.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(bodyPartsResponse.Error);

            var bodyPartsDictionary = new Dictionary<string, List<BodyPartDto>>
            {
                { "Bodies", bodyPartsResponse.Value.Where(x => x.BodyPartTypeId == 1).ToList() },
                { "Noses", bodyPartsResponse.Value.Where(x => x.BodyPartTypeId == 2).ToList() },
                { "Mouths", bodyPartsResponse.Value.Where(x => x.BodyPartTypeId == 3).ToList() },
                { "Eyes", bodyPartsResponse.Value.Where(x => x.BodyPartTypeId == 4).ToList() }
            };

            result.Value = bodyPartsDictionary;
            return result;
        }
    }
}
