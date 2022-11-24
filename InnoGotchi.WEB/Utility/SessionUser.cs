using InnoGotchi.Components.DtoModels;
using InnoGotchi.WEB.Models;

namespace InnoGotchi.WEB.Utility
{
    public class SessionUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public string Image { get; set; }
        public string Token { get; set; }

        public SessionUser() { }
        public SessionUser(AuthenticateResponseDto model)
        {
            if (model != null)
            {
                Id = model.Id;
                Username = model.Username;
                Name = model.Name ?? String.Empty;
                Surname = model.Surname ?? String.Empty;
                Role = model.Role;
                Image = model.Image ?? "~/images/userTemp.png";
                Token = model.Token;
            }
        }

        public void UpdateUserInfo(UserUpdateModel model, string imagePath)
        {
            Name = model.Name;
            Surname = model.Surname;
            Image = imagePath;
        }
    }
}
