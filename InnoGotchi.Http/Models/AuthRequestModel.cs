using System.ComponentModel.DataAnnotations;

namespace InnoGotchi.Http.Models
{
    public class AuthRequestModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string? ConfirmPassword { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Image { get; set; }
        public int? IdentityRoleId { get; set; }
    }
}
