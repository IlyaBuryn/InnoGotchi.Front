using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.BusinessLogic.Interfaces
{
    public interface ICollabService
    {
        public Task<ResponseModel<List<CollaboratorDto>>> GetUserCollabs(int userId);
    }
}
