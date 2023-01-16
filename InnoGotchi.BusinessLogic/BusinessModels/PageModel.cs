using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.BusinessLogic.BusinessModels
{
    public class PageModel
    {
        public PageModel(List<PetDto> petsOnPage, int? totalPets)
        {
            PetsOnPage = petsOnPage;
            TotalPets = totalPets;
        }
        public List<PetDto> PetsOnPage { get; set; }
        public int? TotalPets { get; set; }
    }
}
