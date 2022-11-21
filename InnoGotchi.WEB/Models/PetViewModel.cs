using InnoGotchi.Http.Models;

namespace InnoGotchi.WEB.Models
{
    public class PetViewModel
    {
        public PetViewModel(PetModel pet, bool isReadOnly)
        {
            Pet = pet;
            IsReadOnly = isReadOnly;
        }

        public PetModel Pet { get; set; }
        public bool IsReadOnly { get; set; }
    }
}
