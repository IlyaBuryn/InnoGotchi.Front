using System.ComponentModel.DataAnnotations;

namespace InnoGotchi.Http.Models
{
    public class FarmModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int IdentityUserId { get; set; }

        public IEnumerable<PetModel> Pets { get; set; }
    }
}
