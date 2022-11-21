namespace InnoGotchi.Http.Models
{
    public class VitalSignModel
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public int HungerLevel { get; set; }
        public int ThirsyLevel { get; set; }
        public int HappinessDaysCount { get; set; }
        public bool IsAlive { get; set; }

        public PetModel? Pet { get; set; }
    }
}
