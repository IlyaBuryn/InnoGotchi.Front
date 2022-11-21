using InnoGotchi.Http.Models;

namespace InnoGotchi.Http.Interfaces
{
    public interface IBodyPartService
    {
        public Task<ResponseModel<List<BodyPartModel>>> GetBodyPartsByTypeId(int typeId);
        public Task<ResponseModel<List<BodyPartModel>>> GetBodyParts();
    }
}
