using InnoGotchi.BusinessLogic.SessionEntities;
using System.ComponentModel.DataAnnotations;

namespace InnoGotchi.BusinessLogic.BusinessModels
{
    public class PasswordUpdateModel
    {
        [Required]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Old password is required")]
        [MinLength(6)]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "New password is required")]
        [MinLength(6)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("NewPassword", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmNewPassword { get; set; }

        public PasswordUpdateModel() { }

        public PasswordUpdateModel(SessionUser user)
        {
            UserId = user.Id;
        }
    }
}
