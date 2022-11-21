namespace InnoGotchi.Http.Models
{
    public class CollaboratorModel
    {
        public int Id { get; set; }
        public int FarmId { get; set; }
        public int IdentityUserId { get; set; }

        public FarmModel? Farm { get; set; }
    }
}
