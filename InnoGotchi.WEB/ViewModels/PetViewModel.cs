using InnoGotchi.Components.DtoModels;

namespace InnoGotchi.WEB.ViewModels
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
