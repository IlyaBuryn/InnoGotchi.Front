using System.ComponentModel.DataAnnotations;

namespace InnoGotchi.DataAccess.Models.IdentityModels
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
    }
}
