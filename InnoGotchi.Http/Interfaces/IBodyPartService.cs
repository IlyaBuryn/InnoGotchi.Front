using InnoGotchi.Components.DtoModels;
using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.Interfaces
{
    public interface IBodyPartService
    {
        public Task<ResponseModel<List<BodyPartDto>>> GetBodyPartsByTypeId(int typeId);
        public Task<ResponseModel<List<BodyPartDto>>> GetBodyParts();
    }
}
