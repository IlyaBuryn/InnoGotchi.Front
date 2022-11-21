namespace InnoGotchi.Http.Models
{
    public class BodyPartModel
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string? Color { get; set; }
        public int BodyPartTypeId { get; set; }
        public BodyPartTypeModel? BodyPartType { get; set; }
    }
}
