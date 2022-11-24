using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.WEB.Models
{
    public class PetViewModel
    {
        public PetViewModel(PetDto pet, bool isReadOnly)
        {
            Pet = pet;
            IsReadOnly = isReadOnly;
        }

        public PetDto Pet { get; set; }
        public bool IsReadOnly { get; set; }
    }
}
