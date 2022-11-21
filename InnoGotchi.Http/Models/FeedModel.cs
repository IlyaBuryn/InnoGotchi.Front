namespace InnoGotchi.Http.Models
{
    public class FeedModel
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public int IdentityUserId { get; set; }
        public int FoodCount { get; set; }
        public int WaterCount { get; set; }
        public DateTime FeedTime { get; set; }
    }
}
