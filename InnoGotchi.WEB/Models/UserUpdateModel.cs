using InnoGotchi.WEB.Utility;
using System.ComponentModel.DataAnnotations;

namespace InnoGotchi.WEB.Models
{
    public class UserUpdateModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Surname { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;

        public UserUpdateModel() { }

        public UserUpdateModel(SessionUser user)
        {
            UserId = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            Image = user.Image;
        }
    }
}
