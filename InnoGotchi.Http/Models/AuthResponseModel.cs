using Microsoft.AspNetCore.Http;

namespace InnoGotchi.Http.Models
{
    public class AuthResponseModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string ErrorMessage { get; set; }

    }
}
