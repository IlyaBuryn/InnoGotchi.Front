using System.ComponentModel.DataAnnotations;

namespace InnoGotchi.Http.Models
{
    public class PetModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int FarmId { get; set; }
        public DateTime CreationDate { get; set; }

        public FarmModel Farm { get; set; }
        public VitalSignModel VitalSign { get; set; }
        public List<BodyPartModel> BodyParts { get; set; }
    }
}
