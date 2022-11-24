using System.ComponentModel.DataAnnotations;

namespace InnoGotchi.Http.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Image { get; set; }
        public int IdentityRoleId { get; set; }

        public RegisterModel(AuthRequestModel model)
        {
            Username = model.Username;
            Password = model.Password;
            Name = model.Name;
            Surname = model.Surname;
            Image = model.Image;
            IdentityRoleId = model.IdentityRoleId ?? 0;
        }
    }
}
