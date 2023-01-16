using System.ComponentModel.DataAnnotations;

namespace InnoGotchi.DataAccess.Models.IdentityModels
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public LoginModel()
        {

        }
    }
}
