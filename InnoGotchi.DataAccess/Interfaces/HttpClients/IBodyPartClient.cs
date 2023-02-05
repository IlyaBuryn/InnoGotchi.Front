using InnoGotchi.Components.DtoModels;
using InnoGotchi.DataAccess.Models.ResponseModels;

namespace InnoGotchi.DataAccess.Interfaces.HttpClients
{
    public interface IBodyPartClient
    {
        public Task<ResponseModel<List<BodyPartDto>>> GetBodyPartsByTypeId(int typeId);
        public Task<ResponseModel<List<BodyPartDto>>> GetBodyParts();
    }
}
