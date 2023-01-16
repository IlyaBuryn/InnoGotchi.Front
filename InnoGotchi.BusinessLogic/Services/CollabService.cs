using InnoGotchi.BusinessLogic.Interfaces;
using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Interfaces.HttpClients;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.BusinessLogic.Services
{
    public class CollabService : ICollabService
    {
        private readonly ICollabClient _collabClient;


        public CollabService(ICollabClient collabClient)
        {
            _collabClient = collabClient;
        }


        public async Task<ResponseModel<List<CollaboratorDto>>> GetUserCollabs(int userId)
        {
            var result = new ResponseModel<List<CollaboratorDto>>();

            var friendsResult = await _collabClient.GetCollabsByUser(userId);
            if (friendsResult.ItHasErrorsOrValueIsNull())
                return result.SetAndReturnError(friendsResult.Error);

            result.Value = friendsResult.Value;
            return result;
        }
    }
}
