using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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
    }
}
