using System.ComponentModel.DataAnnotations;

namespace InnoGotchi.Http.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public LoginModel(AuthRequestModel model)
        {
            Username = model.Username;
            Password = model.Password;
        }

        public LoginModel()
        {

        }
    }
}
